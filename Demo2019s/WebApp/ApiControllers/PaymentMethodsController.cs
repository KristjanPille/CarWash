using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{    /// <summary>
    /// PaymentMethods Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PaymentMethodMapper _mapper = new PaymentMethodMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PaymentMethodsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PaymentMethods
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentMethods()
        {
            return Ok((await _bll.PaymentMethods.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/PaymentMethods/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<Payment>> GetPaymentMethod(Guid id)
        {
            var paymentMethod = await _bll.PaymentMethods.FirstOrDefaultAsync(id);

            if (paymentMethod == null)
            {
                return NotFound(new V1DTO.MessageDTO("payment not found"));
            }

            return Ok(_mapper.Map(paymentMethod));
        }

        // PUT: api/PaymentMethods/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutPaymentMethod(Guid id, V1DTO.PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and payment.id do not match"));
            }

            await _bll.PaymentMethods.UpdateAsync(_mapper.Map(paymentMethod));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PaymentMethods
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Payment))]
        public async Task<ActionResult<Payment>> PostPaymentMethod(V1DTO.PaymentMethod paymentMethod)
        {
            var bllEntity = _mapper.Map(paymentMethod);
            _bll.PaymentMethods.Add(bllEntity);
            await _bll.SaveChangesAsync();
            paymentMethod.Id = bllEntity.Id;

            return CreatedAtAction("GetpaymentMethod",
                new {id = paymentMethod.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                paymentMethod);
        }

        // DELETE: api/PaymentMethods/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<Payment>> DeletePaymentMethod(Guid id)
        {
            var paymentMethod = await _bll.PaymentMethods.FirstOrDefaultAsync(id);
            if (paymentMethod == null)
            {
                return NotFound(new V1DTO.MessageDTO("paymentMethod not found"));
            }

            await _bll.PaymentMethods.RemoveAsync(paymentMethod);
            await _bll.SaveChangesAsync();

            return Ok(paymentMethod);
        }
    }
}
