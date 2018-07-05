using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Domain;

namespace Zhiji.Common.Infrastructure
{
    public abstract class RepositoryBase<TContext, TAggreateRoot> : IRepository<TAggreateRoot>
        where TContext : DbContext, IUnitOfWork
        where TAggreateRoot : class, IAggregateRoot
    {
        protected readonly TContext _context;

        protected RepositoryBase(TContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public virtual TAggreateRoot Add(TAggreateRoot entity)
        {
            return _context.Set<TAggreateRoot>().Add(entity).Entity;
        }

        public virtual Task<TAggreateRoot> GetAsync(int id)
        {
            return _context.Set<TAggreateRoot>().FindAsync(id);
        }

        public virtual void Update(TAggreateRoot entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
