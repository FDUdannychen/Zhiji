﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Zhiji.Organization.Domain.Companies;

namespace Zhiji.Organization.Api.Commands.Companies
{
    public class CreateCompanyCommand : IRequest<Company>
    {
        [Required]
        [MaxLength(Company.NameMaxLength)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int? ParentId { get; set; }
    }
}