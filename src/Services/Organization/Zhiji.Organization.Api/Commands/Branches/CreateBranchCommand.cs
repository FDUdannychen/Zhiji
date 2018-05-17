using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Zhiji.Organization.Domain.Branches;

namespace Zhiji.Organization.Api.Commands.Branches
{
    public class CreateBranchCommand : IRequest<Branch>
    {
        [Required]
        [MaxLength(Branch.NameMaxLength)]
        public string Name { get; set; }
    }
}
