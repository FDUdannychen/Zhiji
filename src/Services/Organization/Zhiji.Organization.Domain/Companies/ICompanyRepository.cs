﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organization.Domain.Companies
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> GetAsync(int id);

        Company Add(Company company);

        void Update(Company company);
    }
}
