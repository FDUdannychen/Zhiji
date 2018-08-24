using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Common.Domain;
using Zhiji.Customers.Api.Models.Tenements;
using Zhiji.Customers.Domain.Tenements;

namespace Zhiji.Customers.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TenementsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITenementQuery _tenementQuery;
        private readonly ITenementRepository _tenementRepository;

        public TenementsController(IMapper mapper,
            ITenementQuery tenementQuery,
            ITenementRepository tenementRepository)
        {
            _mapper = mapper;
            _tenementQuery = tenementQuery;
            _tenementRepository = tenementRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewTenement), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewTenement>> GetAsync(int id, CancellationToken cancellationToken)
        {
            var tenement = await _tenementQuery.GetAsync(id, cancellationToken);
            if (tenement is null) return NotFound();
            return _mapper.Map<ViewTenement>(tenement);
        }

        [HttpGet]
        [Route("/customers/{ownerId:int}/[controller]")]
        [ProducesResponseType(typeof(ViewTenement[]), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ViewTenement[]>> ListAllAsync(int ownerId, CancellationToken cancellationToken)
        {
            var tenements = await _tenementQuery.ListAsync(ownerId, cancellationToken);
            return _mapper.Map<ViewTenement[]>(tenements);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewTenement), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateTenement request, CancellationToken cancellationToken)
        {
            var address = _mapper.Map<Domain.Address>(request.Address);
            var tenement = new Tenement(address, request.OwnerId, request.Area, request.TypeId);
            _tenementRepository.Add(tenement);
            await _tenementRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var vm = _mapper.Map<ViewTenement>(tenement);
            return CreatedAtAction(nameof(GetAsync), new { id = vm.Id }, vm);
        }

        [HttpGet]
        [Route("types")]
        public TenementType[] GetAllTenementTypes()
        {
            return Enumeration.GetAll<TenementType>().ToArray();
        }
    }
}
