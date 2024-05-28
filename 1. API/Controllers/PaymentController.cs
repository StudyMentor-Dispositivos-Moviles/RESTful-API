using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1._API.Request;
using _1._API.Response;
using _2._Domain;
using _3._Data;
using _3._Data.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1._API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPaymentDomain _paymentDomain;
        private IPaymentData _paymentData;
        
        //automapper
        private IMapper _mapper;

        public PaymentController(IPaymentDomain paymentDomain, IPaymentData paymentData, IMapper mapper)
        {
            _paymentDomain = paymentDomain;
            _paymentData = paymentData;
            _mapper = mapper;
        }
        // GET: api/Payment
        [HttpGet]
        public async Task<List<PaymentResponse>> GetAsync()
        {
            var payments = await _paymentData.GetAllAsync();
            var paymentResponses = _mapper.Map<List<Payment>, List<PaymentResponse>>(payments);
            return paymentResponses;
        }

        // GET: api/Payment/5
        [HttpGet("{id}", Name = "GetPayment")]
        public Payment Get(int id)
        {
            return _paymentData.GetById(id);
        }

        // POST: api/Payment
        [HttpPost]
        public IActionResult Post([FromBody] PaymentRequest request)
        {
            if (ModelState.IsValid)
            {
                var payment = _mapper.Map<PaymentRequest, Payment>(request);
                return Ok(_paymentDomain.Create(payment));
            }
            else return BadRequest();
        }

        // PUT: api/Payment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Payment/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
