using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zhiji.Common.Domain;
using Zhiji.Organization.Domain.Companies;

namespace Zhiji.Organization.Infrastructure.Repositories
{
    class CompanyRepository : ICompanyRepository
    {
        private readonly OrganizationContext _context;

        public CompanyRepository(OrganizationContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Company Add(Company company)
        {
            return _context.Companies.Add(company).Entity;
        }

        public Task<Company> GetAsync(int id)
        {
            return _context.Companies.FindAsync(id);
        }

        public void Update(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;
        }
    }
}
