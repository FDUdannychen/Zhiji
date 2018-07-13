﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organizations.Domain.Companies
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> GetAsync(int id);

        Task<Company[]> ListAsync();

        Company Add(Company company);

        void Update(Company company);
    }
}