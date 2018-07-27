using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Bills.Domain.Templates
{
    public interface ITemplateRepository : IRepository<Template>
    {
        Task<Template> GetAsync(int id);

        Task<Template[]> ListAsync();

        Template Add(Template template);
    }
}
