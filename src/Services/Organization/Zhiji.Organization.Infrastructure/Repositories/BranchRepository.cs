using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zhiji.Common.Domain;
using Zhiji.Organization.Domain.Branches;

namespace Zhiji.Organization.Infrastructure.Repositories
{
    class BranchRepository : IBranchRepository
    {
        private readonly OrganizationContext _context;

        public BranchRepository(OrganizationContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Branch Add(Branch branch)
        {
            return _context.Branches.Add(branch).Entity;
        }

        public Task<Branch> GetAsync(int id)
        {
            return _context.Branches.FindAsync(id);
        }

        public void Update(Branch branch)
        {
            _context.Entry(branch).State = EntityState.Modified;
        }
    }
}
