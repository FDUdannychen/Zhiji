using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Domain;

namespace Zhiji.Organization.Infrastructure.Repositories
{
    abstract class RepositoryBase<T> : IRepository<T> where T : class, IAggregateRoot
    {
        protected readonly OrganizationContext _context;

        protected RepositoryBase(OrganizationContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public virtual T Add(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public virtual Task<T> GetAsync(int id)
        {
            return _context.Set<T>().FindAsync(id);
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
