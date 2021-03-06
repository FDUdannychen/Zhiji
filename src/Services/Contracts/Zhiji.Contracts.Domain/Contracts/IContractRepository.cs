﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Contracts
{
    public interface IContractRepository : IRepository
    {
        Task<Contract> GetAsync(int id, CancellationToken cancellationToken = default);

        Contract Add(Contract contract);
    }
}
