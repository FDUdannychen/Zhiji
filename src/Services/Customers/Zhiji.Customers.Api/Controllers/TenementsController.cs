using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Common.Api;
using Zhiji.Common.Domain;
using Zhiji.Customers.Api.Models.Customers;
using Zhiji.Customers.Api.Models.Tenements;
using Zhiji.Customers.Domain.Tenements;

namespace Zhiji.Customers.Api.Controllers
{
    public class TenementsController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITenementRepository _tenementRepository;

        public TenementsController(IMapper mapper,
            ITenementRepository tenementRepository)
        {
            _mapper = mapper;
            _tenementRepository = tenementRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewTenement), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewTenement>> Get(int id)
        {
            var tenement = await _tenementRepository.GetAsync(id);
            if (tenement is null) return NotFound();
            return _mapper.Map<ViewTenement>(tenement);
        }

        [HttpGet]
        [Route("/customers/{ownerId:int}/[controller]")]
        [ProducesResponseType(typeof(ViewTenement[]), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ViewTenement[]>> GetAll(int ownerId)
        {
            var tenements = await _tenementRepository.ListAsync(ownerId);
            return _mapper.Map<ViewTenement[]>(tenements);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewTenement), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody]CreateTenement request)
        {
            var address = _mapper.Map<Domain.Address>(request.Address);
            var tenement = new Tenement(address, request.OwnerId, request.Area, request.TypeId);
            _tenementRepository.Add(tenement);
            await _tenementRepository.UnitOfWork.SaveChangesAsync();
            var vm = _mapper.Map<ViewTenement>(tenement);
            return CreatedAtAction(nameof(Get), new { id = vm.Id }, vm);
        }

        [HttpGet]
        [Route("types")]
        public TenementType[] GetAllTenementTypes()
        {
            return Enumeration.GetAll<TenementType>().ToArray();
        }
    }
}
