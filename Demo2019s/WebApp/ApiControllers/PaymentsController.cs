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
    /// Payments Api Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PaymentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PaymentMapper _mapper = new PaymentMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PaymentsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Payments
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            return Ok((await _bll.Payments.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<Payment>> GetPayment(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultAsync(id);

            if (payment == null)
            {
                return NotFound(new V1DTO.MessageDTO("payment not found"));
            }

            return Ok(_mapper.Map(payment));
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutPayment(Guid id, V1DTO.Payment payment)
        {
            if (id != payment.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and payment.id do not match"));
            }

            await _bll.Payments.UpdateAsync(_mapper.Map(payment));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Payments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Payment))]
        public async Task<ActionResult<Payment>> PostPayment(V1DTO.Payment payment)
        {
            var bllEntity = _mapper.Map(payment);
            _bll.Payments.Add(bllEntity);
            await _bll.SaveChangesAsync();
            payment.Id = bllEntity.Id;

            return CreatedAtAction("Getpayment",
                new {id = payment.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                payment);
        }

        // DELETE: api/Payments/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<Payment>> DeletePayment(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultAsync(id);
            if (payment == null)
            {
                return NotFound(new V1DTO.MessageDTO("payment not found"));
            }

            await _bll.Payments.RemoveAsync(payment);
            await _bll.SaveChangesAsync();

            return Ok(payment);
        }
    }
}
