using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Zhiji.Bills.Api.Models.Bills;
using Zhiji.Bills.Domain.Bills;

namespace Zhiji.Bills.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBillQuery _billQuery;

        public BillsController(IMapper mapper,
            IBillQuery billQuery)
        {
            _mapper = mapper;
            _billQuery = billQuery;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ViewBill[]), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ViewBill[]>> SearchAsync([FromQuery]SearchBill request, CancellationToken cancellationToken)
        {
            var Bills = await _billQuery.ListAsync(
                request.CustomerId, 
                request.TenementId, 
                request.ContractId,
                request.TemplateId,
                request.StartDateRange,
                request.EndDateRange,
                request.StatusId,
                cancellationToken);

            return _mapper.Map<ViewBill[]>(Bills);
        }
    }
}
