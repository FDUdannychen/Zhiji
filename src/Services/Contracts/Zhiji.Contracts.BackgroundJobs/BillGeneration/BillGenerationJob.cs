using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Zhiji.Contracts.Domain.Bills;
using Zhiji.Contracts.Domain.Contracts;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.BackgroundJobs.BillGeneration
{
    public class BillGenerationJob : IntervalJob
    {
        private readonly IContractRepository _contractRepository;
        private readonly IBillRepository _billRepository;

        public BillGenerationJob(
            IContractRepository contractRepository,
            IBillRepository billRepository,
            IOptions<BillGenerationOptions> options)
            : base(options.Value.Interval)
        {
            _contractRepository = contractRepository;
            _billRepository = billRepository;
        }

        protected override async Task ExecutePeriodicallyAsync(CancellationToken stoppingToken)
        {
            var contracts = await _contractRepository.ListEffectiveAsync(stoppingToken);

            foreach (var contract in contracts)
            {
                var bills = await _billRepository.ListAsync(contract.Id, stoppingToken);
                var allPeriods = GetBillingPeriods(contract);
                var missingPeriods = allPeriods.Where(p => bills.All(b => !b.Period.Equals(p)));

                foreach (var period in missingPeriods)
                {
                    var bill = new Bill(contract.Template.Name, contract.Id, period);
                    _billRepository.Add(bill);
                }
            }

            await _billRepository.UnitOfWork.SaveChangesAsync(stoppingToken);
        }

        private IEnumerable<BillingPeriod> GetBillingPeriods(Contract contract)
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

            while (billingDate < contract.StartTime)
            {
                billingDate = billingDate.AddMonths(step);
            }

            var today = DateTimeOffset.UtcNow.Date;
            while (billingDate <= today && (!contract.EndTime.HasValue || billingDate <= contract.EndTime.Value))
            {
                var nextBillingDate = billingDate.AddMonths(step);
                yield return new BillingPeriod(billingDate, nextBillingDate);
                billingDate = nextBillingDate;
            }
        }
    }
}
