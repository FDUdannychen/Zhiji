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
using Zhiji.IntegrationEventLog;

namespace Zhiji.Contracts.BackgroundJobs.BillingDateCheck
{
    public class BillingDateCheckJob : IntervalJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Func<DbConnection, IIntegrationEventService> _integrationEventServiceFactory;

        public BillingDateCheckJob(
            IServiceScopeFactory serviceScopeFactory,
            Func<DbConnection, IIntegrationEventService> integrationEventServiceFactory,
            IOptions<BillingDateCheckOptions> options,
            ILogger<BillingDateCheckJob> logger)
            : base(options.Value.Interval, logger)
        {
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
                    //var events = GetEvents(contract, lastCreateTime);

                }
            }
        }

        //private IEnumerable<BillingDateReachedIntegrationEvent> GetEvents(Contract contract, Instant lastTime)
        //{
        //    var billingMode = contract.Template.BillingMode;
        //    var billingDate = contract.Template.BillingDate;            
        //    var date = new LocalDate(contract.StartDate.Year, billingDate.Month, billingDate.Day);
        //    var lastDate = lastTime.InZone(contract.Template.TimeZone).Date;

        //    if (billingMode.Equals(BillingMode.Prepaid))
        //    {
        //        while (date > contract.StartDate) date = date.PlusMonths(-billingDate.IntervalMonth);
        //        while(date.PlusMonths(billingDate.IntervalMonth))
        //    }
        //}
    }
}
