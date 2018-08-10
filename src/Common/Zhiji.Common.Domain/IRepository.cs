using System;
using System.Collections.Generic;
using System.Text;

namespace Zhiji.Common.Domain
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
