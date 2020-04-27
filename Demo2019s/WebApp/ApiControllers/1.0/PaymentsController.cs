using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using Payment = Domain.Payment;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PaymentsController(IAppBLL bll)
        {
            _bll = bll;
        }
        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            var payment = (await _bll.Payments.AllAsync())
                .Select(bllEntity => new Payment()
                {
                    Id = bllEntity.Id,
                    PaymentAmount = bllEntity.PaymentAmount,
                }) ;
            
            return Ok(payment);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultAsync(id, User.UserGuidId());

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(Guid id, Payment paymentEditDTO)
        {
            if (id != paymentEditDTO.Id)
            {
                return BadRequest();
            }

            var payment = await _bll.Payments.FirstOrDefaultAsync(paymentEditDTO.Id, User.UserGuidId());
            if (payment == null)
            {
                return BadRequest();
            }

            paymentEditDTO.ChangedAt = paymentEditDTO.ChangedAt;
            paymentEditDTO.CreatedAt = paymentEditDTO.CreatedAt;
            paymentEditDTO.TimeOfPayment = paymentEditDTO.TimeOfPayment;
            paymentEditDTO.PaymentAmount = paymentEditDTO.PaymentAmount;

            _bll.Payments.Update(payment);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Payments.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Payments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(PaymentCreate paymentCreate)
        {
            var payment = new BLL.App.DTO.Payment()
            {
                AppUserId = User.UserGuidId(),
                PaymentAmount = paymentCreate.PaymentAmount,
                TimeOfPayment = paymentCreate.TimeOfPayment,
            };

            _bll.Payments.Add(payment);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPayment", new {id = payment.Id}, payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Payment>> DeletePayment(Guid id)
        {
            var payment = await _bll.Payments 
                .FirstOrDefaultAsync(id, User.UserGuidId());
            if (payment == null)
            {
                return NotFound();
            }

            _bll.Payments.Remove(payment);
            await _bll.SaveChangesAsync();

            return Ok(payment);
        }
    }
}
