using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Organization.Api.Commands.Departments;
using Zhiji.Organization.Api.ViewModels;

namespace Zhiji.Organization.Api.Controllers
{    
    public class DepartmentsController : OrganizationControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DepartmentsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(DepartmentViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DepartmentViewModel), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DepartmentViewModel>> Get(int id)
        {
            var request = new QueryDepartmentCommand { Id = id };
            var response = await _mediator.Send(request);
            var department = response.SingleOrDefault();

            if (department is null) return NotFound();

            return _mapper.Map<DepartmentViewModel>(department);
        }

        [HttpGet]
        [Route("/companies/{companyId:int}/[controller]")]
        [ProducesResponseType(typeof(IEnumerable<DepartmentViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<DepartmentViewModel>> GetAll(int companyId)
        {
            var request = new QueryDepartmentCommand();
            var response = await _mediator.Send(request);
            return _mapper.Map<IEnumerable<DepartmentViewModel>>(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DepartmentViewModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody]CreateDepartmentCommand request)
        {
            var department = await _mediator.Send(request);
            var vm = _mapper.Map<DepartmentViewModel>(department);
            return CreatedAtAction(nameof(Get), new { id = vm.Id }, vm);
        }
    }
}
