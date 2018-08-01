using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Zhiji.Contracts.Domain.Bills;
using Zhiji.Contracts.Domain.Contracts;

namespace Zhiji.Contracts.BackgroundJobs
{
    public class BillGenerationJob : IntervalJob
    {
        private readonly IContractRepository _contractRepository;
        private readonly IBillRepository _billRepository;

        public BillGenerationJob(
            IContractRepository contractRepository,
            IBillRepository billRepository)
            : base(TimeSpan.FromHours(6))
        {
            _contractRepository = contractRepository;
            _billRepository = billRepository;
        }

        protected override async Task ExecutePeriodicallyAsync(CancellationToken stoppingToken)
        {
            var contracts = await _contractRepository.ListEffectiveAsync();

            foreach (var contract in contracts)
            {
                var bills = await _billRepository.ListBillsAsync(contract.Id);
                var missingPeirods = GetMissingPeriods(contract, bills);
            }
        }

        private List<BillingPeriod> GetMissingPeriods(Contract contract, Bill[] bills)
        {
            throw new NotImplementedException();
        }

        private DateTime GetMostRecentPastBillingDate(Contract contract)
        {
            var today = DateTime.UtcNow.Date;
            var last = new DateTime(today.Year, 12, contract.Template.BillingDate.Day);
            throw new NotImplementedException();

        }
    }
}
