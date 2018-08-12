﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Bills
{
    public interface IBillRepository : IRepository
    {
        Task<Bill> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<Bill[]> ListAsync(int contractId, CancellationToken cancellationToken = default);

        Bill Add(Bill bill);
    }
}
