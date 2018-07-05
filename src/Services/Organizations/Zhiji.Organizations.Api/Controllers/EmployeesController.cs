using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Common.AspNetCore;
using Zhiji.Organizations.Api.Models;
using Zhiji.Organizations.Domain.Employees;

namespace Zhiji.Organizations.Api.Controllers
{    
    public class EmployeesController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IMapper mapper,
            IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewEmployee), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ViewEmployee), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewEmployee>> Get(int id)
        {
            var employee = await _employeeRepository.GetAsync(id);
            if (employee is null) return NotFound();
            return _mapper.Map<ViewEmployee>(employee);
        }

        [HttpGet]
        [Route("/companies/{companyId:int}/[controller]")]
        [ProducesResponseType(typeof(IEnumerable<ViewEmployee>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<ViewEmployee>> ListByCompany(int companyId)
        {
            var employees = await _employeeRepository.ListByCompanyAsync(companyId);
            return _mapper.Map<IEnumerable<ViewEmployee>>(employees);
        }

        [HttpGet]
        [Route("/departments/{departmentId:int}/[controller]")]
        [ProducesResponseType(typeof(IEnumerable<ViewEmployee>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<ViewEmployee>> ListByDepartment(int departmentId)
        {
            var employees = await _employeeRepository.ListByDepartmentAsync(departmentId);
            return _mapper.Map<IEnumerable<ViewEmployee>>(employees);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewEmployee), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody]CreateEmployee request)
        {
            var employee = new Employee(request.Name, request.DepartmentId);
            _employeeRepository.Add(employee);
            await _employeeRepository.UnitOfWork.SaveChangesAsync();
            var vm = _mapper.Map<ViewEmployee>(employee);
            return CreatedAtAction(nameof(Get), new { id = vm.Id }, vm);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody]UpdateEmployee request)
        {
            var employee = await _employeeRepository.GetAsync(request.EmployeeId);
            if (employee is null) return NotFound();

            if (request.Name != null) employee.ChangeName(request.Name);
            if (request.DepartmentId.HasValue) employee.TransferDepartment(request.DepartmentId.Value);

            await _employeeRepository.UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
