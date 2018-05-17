using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Organization.Api.Commands.Branches;
using Zhiji.Organization.Domain.Branches;

namespace Zhiji.Organization.Api.Controllers
{
    [Route("[controller]")]
    public class BranchesController : Controller
    {
        private readonly IMediator _mediator;

        public BranchesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Branch), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            var request = new QueryBranchCommand { Id = id };
            var response = await _mediator.Send(request);
            var branch = response.SingleOrDefault();

            if (branch == null) return NotFound();
            return Ok(branch);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Branch), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]CreateBranchCommand request)
        {
            var branch = await _mediator.Send(request);
            return Ok(branch);
        }
    }
}
