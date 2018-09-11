using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NodaTime;
using Zhiji.Contracts.Domain.Contracts;
using Zhiji.Contracts.Domain.Templates;
using Zhiji.Contracts.Infrastructure;
using Zhiji.EventBus;
using Zhiji.EventBus.IntegrationEvents;
using Zhiji.IntegrationEventLog;

namespace Zhiji.Contracts.BackgroundJobs.BillingDateCheck
{
    class BillingDateCheckJob : IntervalJob
    {
        private readonly IClock _clock;
        private readonly IEventBus _eventBus;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Func<DbConnection, IIntegrationEventService> _integrationEventServiceFactory;

        public BillingDateCheckJob(
            IClock clock,
            IEventBus eventBus,
            IServiceScopeFactory serviceScopeFactory,
            Func<DbConnection, IIntegrationEventService> integrationEventServiceFactory,
            IOptions<BillingDateCheckOptions> options,
            ILogger<BillingDateCheckJob> logger)
            : base(options.Value.Interval, logger)
        {
            _clock = clock;
            _eventBus = eventBus;
            _serviceScopeFactory = serviceScopeFactory;
            _integrationEventServiceFactory = integrationEventServiceFactory;
        }

        protected override async Task ExecutePeriodicallyAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var contractQuery = scope.ServiceProvider.GetService<IContractQuery>();
                var contractContext = scope.ServiceProvider.GetService<ContractContext>();
                var integrationEventService = _integrationEventServiceFactory(contractContext.Database.GetDbConnection());

                var lastCreateTime = await integrationEventService.GetLastCreateTimeAsync<BillingDateReachedIntegrationEvent>(stoppingToken);
                var contracts = await contractQuery.ListEffectiveAsync(stoppingToken);

                foreach (var contract in contracts)
                {
                    var events = GetEvents(contract, lastCreateTime);
                    foreach (var evt in events)
                    {
                        await integrationEventService.SaveEventAsync(evt, cancellationToken: stoppingToken);
                        await _eventBus.PublishAsync(evt, stoppingToken);                        
                        await integrationEventService.MarkEventPublishedAsync(evt, stoppingToken);
                    }
                }
            }
        }

        private IEnumerable<BillingDateReachedIntegrationEvent> GetEvents(Contract contract, Instant lastCreateTime)
        {
            var timeZone = contract.Template.TimeZone;
            var lastDate = lastCreateTime.InZone(timeZone).Date;
            var currentDate = _clock.GetCurrentInstant().InZone(timeZone).Date;

            if (lastDate >= currentDate) yield break;

            var billingMode = contract.Template.BillingMode;
            var billingDate = contract.Template.BillingDate;
            var startDate = contract.StartDate.InZone(timeZone).Date;
            var nextBillingDate = new LocalDate(startDate.Year, billingDate.Month, billingDate.Day);
            var monthOffset = contract.Template.BillingPeriodStartMonthOffset;

            while (nextBillingDate > startDate)
                nextBillingDate = nextBillingDate.PlusMonths(-billingDate.IntervalMonth);

            while (nextBillingDate.PlusMonths(billingDate.IntervalMonth + monthOffset) <= startDate)
                nextBillingDate = nextBillingDate.PlusMonths(billingDate.IntervalMonth);

            if (billingMode.Equals(BillingMode.Postpaid))
            {
                nextBillingDate = nextBillingDate.PlusMonths(-billingDate.IntervalMonth);
            }

            while (nextBillingDate < currentDate)
            {
                if (nextBillingDate > lastDate)
                {
                    var billingStart = nextBillingDate.PlusMonths(contract.Template.BillingPeriodStartMonthOffset);
                    var billingEnd = billingStart.PlusMonths(contract.Template.BillingPeriodMonth);

                    yield return new BillingDateReachedIntegrationEvent(
                        contract.Id,
                        nextBillingDate.AtStartOfDayInZone(timeZone).ToInstant(),
                        billingStart.AtStartOfDayInZone(timeZone).ToInstant(),
                        billingEnd.AtStartOfDayInZone(timeZone).ToInstant());
                }

                nextBillingDate = nextBillingDate.PlusMonths(billingDate.IntervalMonth);
            }
        }
    }
}
