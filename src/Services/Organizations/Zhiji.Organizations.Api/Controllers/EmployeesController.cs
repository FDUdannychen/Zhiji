using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Organizations.Api.Models.Employees;
using Zhiji.Organizations.Domain.Employees;

namespace Zhiji.Organizations.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeQuery _employeeQuery;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IMapper mapper,
            IEmployeeQuery employeeQuery,
            IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeQuery = employeeQuery;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewEmployee), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ViewEmployee), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewEmployee>> GetAsync(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeQuery.GetAsync(id, cancellationToken);
            if (employee is null) return NotFound();
            return _mapper.Map<ViewEmployee>(employee);
        }

        [HttpGet]
        [Route("/companies/{companyId:int}/[controller]")]
        [ProducesResponseType(typeof(ViewEmployee[]), (int)HttpStatusCode.OK)]
        public async Task<ViewEmployee[]> ListByCompanyAsync(int companyId, CancellationToken cancellationToken)
        {
            var employees = await _employeeQuery.ListByCompanyAsync(companyId, cancellationToken);
            return _mapper.Map<ViewEmployee[]>(employees);
        }

        [HttpGet]
        [Route("/departments/{departmentId:int}/[controller]")]
        [ProducesResponseType(typeof(ViewEmployee[]), (int)HttpStatusCode.OK)]
        public async Task<ViewEmployee[]> ListByDepartmentAsync(int departmentId, CancellationToken cancellationToken)
        {
            var employees = await _employeeQuery.ListByDepartmentAsync(departmentId, cancellationToken);
            return _mapper.Map<ViewEmployee[]>(employees);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewEmployee), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateEmployee request, CancellationToken cancellationToken)
        {
            var employee = new Employee(request.Name, request.DepartmentId);
            _employeeRepository.Add(employee);
            await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var vm = _mapper.Map<ViewEmployee>(employee);
            return CreatedAtAction(nameof(GetAsync), new { id = vm.Id }, vm);
        }

        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]        
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateEmployee request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetAsync(request.EmployeeId, cancellationToken);
            if (employee is null) return NotFound();

            if (request.Name != null) employee.ChangeName(request.Name);
            if (request.DepartmentId.HasValue) employee.TransferDepartment(request.DepartmentId.Value);

            await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
