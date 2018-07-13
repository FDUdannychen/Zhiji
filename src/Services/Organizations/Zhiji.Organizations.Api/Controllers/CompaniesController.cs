using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Common.Api;
using Zhiji.Organizations.Api.Models.Companies;
using Zhiji.Organizations.Domain.Companies;

namespace Zhiji.Organizations.Api.Controllers
{
    public class CompaniesController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(IMapper mapper, 
            ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewCompany), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewCompany>> Get(int id)
        {
            var company = await _companyRepository.GetAsync(id);
            if (company is null) return NotFound();
            return _mapper.Map<ViewCompany>(company);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ViewCompany[]), (int)HttpStatusCode.OK)]
        public async Task<ViewCompany[]> GetAll()
        {
            var companies = await _companyRepository.ListAsync();
            return _mapper.Map<ViewCompany[]>(companies);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewCompany), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody]CreateCompany request)
        {
            var company = new Company(request.Name, request.ParentId);
            _companyRepository.Add(company);
            await _companyRepository.UnitOfWork.SaveChangesAsync();
            var vm = _mapper.Map<ViewCompany>(company);
            return CreatedAtAction(nameof(Get), new { id = vm.Id }, vm);
        }
    }
}
