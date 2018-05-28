using System;
using System.Collections.Generic;
using System.Text;

namespace Zhiji.Common.Domain
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }

    public interface IRepository<T> : IRepository
        where T : IAggregateRoot
    { }
}
