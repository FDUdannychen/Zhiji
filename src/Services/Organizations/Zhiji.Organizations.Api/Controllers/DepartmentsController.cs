using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Organizations.Api.Models.Departments;
using Zhiji.Organizations.Domain.Departments;

namespace Zhiji.Organizations.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentQuery _departmentQuery;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IMapper mapper,
            IDepartmentQuery departmentQuery,
            IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentQuery = departmentQuery;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewDepartment), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewDepartment>> GetAsync(int id, CancellationToken cancellationToken)
        {
            var department = await _departmentQuery.GetAsync(id, cancellationToken);
            if (department is null) return NotFound();
            return _mapper.Map<ViewDepartment>(department);
        }

        [HttpGet]
        [Route("/companies/{companyId:int}/[controller]")]
        [ProducesResponseType(typeof(ViewDepartment[]), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ViewDepartment[]>> ListAllAsync(int companyId, CancellationToken cancellationToken)
        {
            var departments = await _departmentQuery.ListAsync(companyId, cancellationToken);
            return _mapper.Map<ViewDepartment[]>(departments);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewDepartment), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateDepartment request, CancellationToken cancellationToken)
        {
            var department = new Department(request.Name, request.ParentId, request.CompanyId);
            _departmentRepository.Add(department);
            await _departmentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var vm = _mapper.Map<ViewDepartment>(department);
            return CreatedAtAction(nameof(GetAsync), new { id = vm.Id }, vm);
        }
    }
}
