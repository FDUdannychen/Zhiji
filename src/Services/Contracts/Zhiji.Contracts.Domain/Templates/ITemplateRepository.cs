using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Templates
{
    public interface ITemplateRepository : IRepository
    {
        Task<Template> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<Template[]> ListAsync(CancellationToken cancellationToken = default);

        Template Add(Template template);
    }
}
