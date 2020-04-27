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
using PaymentMethod = Domain.PaymentMethod;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PaymentMethodsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PaymentMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
        {
            var paymentMethod = (await _bll.PaymentMethods.AllAsync())
                .Select(bllEntity => new PaymentMethod()
                {
                    Id = bllEntity.Id,
                    PaymentMethodName = bllEntity.PaymentMethodName,
                }) ;
            
            return Ok(paymentMethod);
        }

        // GET: api/PaymentMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(Guid id)
        {
            var paymentMethod = await _bll.PaymentMethods.FirstOrDefaultAsync(id, User.UserGuidId());

            if (paymentMethod == null)
            {
                return NotFound();
            }

            return Ok(paymentMethod);
        }

        // PUT: api/PaymentMethods/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(Guid id, PaymentMethod paymentMethodEditDTO)
        {
            if (id != paymentMethodEditDTO.Id)
            {
                return BadRequest();
            }

            var paymentMethod = await _bll.PaymentMethods.FirstOrDefaultAsync(paymentMethodEditDTO.Id, User.UserGuidId());
            if (paymentMethod == null)
            {
                return BadRequest();
            }

            paymentMethodEditDTO.ChangedAt = paymentMethodEditDTO.ChangedAt;
            paymentMethodEditDTO.CreatedAt = paymentMethodEditDTO.CreatedAt;

            _bll.PaymentMethods.Update(paymentMethod);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.PaymentMethods.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/PaymentMethods
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethodCreate paymentMethodCreate)
        {
            var paymentMethod = new BLL.App.DTO.PaymentMethod()
            {
                AppUserId = User.UserGuidId(),
                PaymentMethodName = paymentMethodCreate.PaymentMethodName,
            };

            _bll.PaymentMethods.Add(paymentMethod);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPaymentMethod", new {id = paymentMethod.Id}, paymentMethod);
        }

        // DELETE: api/PaymentMethods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentMethod>> DeletePaymentMethod(Guid id)
        {
            var paymentMethod = await _bll.PaymentMethods 
                .FirstOrDefaultAsync(id, User.UserGuidId());
            if (paymentMethod == null)
            {
                return NotFound();
            }

            _bll.PaymentMethods.Remove(paymentMethod);
            await _bll.SaveChangesAsync();

            return Ok(paymentMethod);
        }
    }
}
