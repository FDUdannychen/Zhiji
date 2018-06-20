using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zhiji.Common.Domain;
using Zhiji.Organization.Domain.Departments;

namespace Zhiji.Organization.Infrastructure.Repositories
{
    class DepartmentRepository : IDepartmentRepository
    {
        private readonly OrganizationContext _context;

        public DepartmentRepository(OrganizationContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Department Add(Department department)
        {
            return _context.Departments.Add(department).Entity;
        }

        public Task<Department> GetAsync(int id)
        {
            return _context.Departments.FindAsync(id);
        }

        public void Update(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
        }
    }
}
