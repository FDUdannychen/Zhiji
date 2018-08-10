using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Common.Api;
using Zhiji.Bills.Api.Models.Bills;
using Zhiji.Bills.Domain.Bills;

namespace Zhiji.Bills.Api.Controllers
{
    public class BillsController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBillRepository _billRepository;

        public BillsController(IMapper mapper,
            IBillRepository billRepository)
        {
            _mapper = mapper;
            _billRepository = billRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewBill), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewBill>> Get(int id)
        {
            var bill = await _billRepository.GetAsync(id);
            if (bill is null) return NotFound();
            return _mapper.Map<ViewBill>(bill);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ViewBill[]), (int)HttpStatusCode.OK)]
        public async Task<ViewBill[]> Search(SearchBill request)
        {
            var bills = await _billRepository.ListAsync(request.CustomerId, request.TenementId, request.ContractId, request.TemplateId, request.BillStatusId);
            return _mapper.Map<ViewBill[]>(bills);
        }
    }
}
