using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Domain;

namespace Zhiji.Common.EntityFrameworkCore
{
    public abstract class RepositoryBase<TContext, TAggreateRoot> : IRepository
        where TContext : DbContext, IUnitOfWork
        where TAggreateRoot : class, IAggregateRoot
    {
        protected readonly TContext _context;

        protected RepositoryBase(TContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public virtual Task<TAggreateRoot> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Set<TAggreateRoot>().FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual TAggreateRoot Add(TAggreateRoot entity)
        {
            return _context.Set<TAggreateRoot>().Add(entity).Entity;
        }

        public virtual void Update(TAggreateRoot entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
