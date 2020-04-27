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
using Discount = Domain.Discount;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DiscountsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public DiscountsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Discounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discount>>> GetDiscounts()
        {
            var discount = (await _bll.Discounts.AllAsync())
                .Select(bllEntity => new Discount()
                {
                    Id = bllEntity.Id,
                    DiscountAmount = bllEntity.DiscountAmount,
                }) ;
            
            return Ok(discount);
        }

        // GET: api/Discounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Discount>> GetDiscount(Guid id)
        {
            var discount = await _bll.Discounts.FirstOrDefaultAsync(id, User.UserGuidId());

            if (discount == null)
            {
                return NotFound();
            }

            return Ok(discount);
        }

        // PUT: api/Discounts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscount(Guid id, Discount discountEditDTO)
        {
            if (id != discountEditDTO.Id)
            {
                return BadRequest();
            }

            var discount = await _bll.Discounts.FirstOrDefaultAsync(discountEditDTO.Id, User.UserGuidId());
            if (discount == null)
            {
                return BadRequest();
            }

            discount.DiscountAmount = discountEditDTO.DiscountAmount;

            _bll.Discounts.Update(discount);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Discounts.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Discounts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Discount>> PostDiscount(DiscountCreate discountCreateDTO)
        {
            var discount = new BLL.App.DTO.Discount()
            {
                AppUserId = User.UserGuidId(),
                DiscountAmount = discountCreateDTO.DiscountAmount,
            };

            _bll.Discounts.Add(discount);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetDiscount", new {id = discount.Id}, discount);
        }

        // DELETE: api/Discounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Discount>> DeleteDiscount(Guid id)
        {
            var discount = await _bll.Discounts 
                .FirstOrDefaultAsync(id, User.UserGuidId());
            if (discount == null)
            {
                return NotFound();
            }

            _bll.Discounts.Remove(discount);
            await _bll.SaveChangesAsync();

            return Ok(discount);
        }
    }
}
