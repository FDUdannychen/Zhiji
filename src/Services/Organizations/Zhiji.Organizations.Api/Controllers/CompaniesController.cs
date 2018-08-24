using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Organizations.Api.Models.Companies;
using Zhiji.Organizations.Domain.Companies;

namespace Zhiji.Organizations.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyQuery _companyQuery;
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(IMapper mapper,
            ICompanyQuery companyQuery,
            ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _companyQuery = companyQuery;
            _companyRepository = companyRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewCompany), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewCompany>> GetAsync(int id, CancellationToken cancellationToken)
        {
            var company = await _companyQuery.GetAsync(id, cancellationToken);
            if (company is null) return NotFound();
            return _mapper.Map<ViewCompany>(company);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ViewCompany[]), (int)HttpStatusCode.OK)]
        public async Task<ViewCompany[]> ListAllAsync(CancellationToken cancellationToken)
        {
            var companies = await _companyQuery.ListAsync(cancellationToken);
            return _mapper.Map<ViewCompany[]>(companies);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewCompany), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateCompany request, CancellationToken cancellationToken)
        {
            var company = new Company(request.Name, request.ParentId);
            _companyRepository.Add(company);
            await _companyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var vm = _mapper.Map<ViewCompany>(company);
            return CreatedAtAction(nameof(GetAsync), new { id = vm.Id }, vm);
        }
    }
}
