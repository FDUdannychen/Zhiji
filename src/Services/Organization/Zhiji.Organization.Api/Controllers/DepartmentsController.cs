using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Organization.Api.Models;
using Zhiji.Organization.Domain.Departments;

namespace Zhiji.Organization.Api.Controllers
{    
    public class DepartmentsController : OrganizationControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IMapper mapper, 
            IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewDepartment), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ViewDepartment), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewDepartment>> Get(int id)
        {
            var department = await _departmentRepository.GetAsync(id);
            if (department is null) return NotFound();
            return _mapper.Map<ViewDepartment>(department);
        }

        [HttpGet]
        [Route("/companies/{companyId:int}/[controller]")]
        [ProducesResponseType(typeof(IEnumerable<ViewDepartment>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<ViewDepartment>> GetAll(int companyId)
        {
            var departments = await _departmentRepository.ListAsync(companyId);
            return _mapper.Map<IEnumerable<ViewDepartment>>(departments);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewDepartment), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody]CreateDepartment request)
        {
            var department = new Department(request.Name, request.ParentId, request.CompanyId);
            _departmentRepository.Add(department);
            await _departmentRepository.UnitOfWork.SaveChangesAsync();
            var vm = _mapper.Map<ViewDepartment>(department);
            return CreatedAtAction(nameof(Get), new { id = vm.Id }, vm);
        }
    }
}
