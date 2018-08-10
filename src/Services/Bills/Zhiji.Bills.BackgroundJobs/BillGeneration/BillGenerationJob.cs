﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zhiji.Bills.Domain.Bills;
using Zhiji.Bills.Domain.Contracts;
using Zhiji.Bills.Domain.Templates;

namespace Zhiji.Bills.BackgroundJobs.BillGeneration
{
    public class BillGenerationJob : IntervalJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BillGenerationJob(
            IServiceScopeFactory serviceScopeFactory, 
            IOptions<BillGenerationOptions> options,
            ILogger<BillGenerationJob> logger)
            : base(options.Value.Interval, logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecutePeriodicallyAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var contractRepository = scope.ServiceProvider.GetService<IContractRepository>();
                var billRepository = scope.ServiceProvider.GetService<IBillRepository>();
                var contracts = await contractRepository.ListEffectiveAsync(stoppingToken);

                foreach (var contract in contracts)
                {
                    var bills = await billRepository.ListAsync(contractId: contract.Id, cancellationToken: stoppingToken);
                    var allPeriods = GetBillPeriods(contract);
                    var missingPeriods = allPeriods.Where(p => bills.All(b => !b.Period.Equals(p)));

                    foreach (var period in missingPeriods)
                    {
                        var bill = new Bill(contract.Template.Name, contract.Id, period);
                        billRepository.Add(bill);
                    }
                }

                await billRepository.UnitOfWork.SaveChangesAsync(stoppingToken);
            }
        }

        private IEnumerable<BillPeriod> GetBillPeriods(Contract contract)
        {
            var template = contract.Template;
            int year = contract.StartTime.Year, month, day = template.BillingDate.Day, step;

            if (template.BillingMode.Equals(BillingMode.Year))
            {
                month = template.BillingDate.Month.Value;
                step = 12;
            }
            else if (template.BillingMode.Equals(BillingMode.Quarter))
            {
                month = template.BillingDate.Month.Value;
                step = 3;
            }
            else if (template.BillingMode.Equals(BillingMode.Month))
            {
                month = contract.StartTime.Month;
                step = 1;
            }
            else throw new NotSupportedException();

            var billingDate = new DateTimeOffset(year, month, day, 0, 0, 0, TimeSpan.Zero);

            while (billingDate > contract.StartTime)
            {
                billingDate = billingDate.AddMonths(-step);
            }

            var today = DateTimeOffset.UtcNow.Date;
            while (billingDate <= today && (!contract.EndTime.HasValue || billingDate <= contract.EndTime.Value))
            {
                var nextBillingDate = billingDate.AddMonths(step);
                yield return new BillPeriod(billingDate, nextBillingDate);
                billingDate = nextBillingDate;
            }
        }
    }
}