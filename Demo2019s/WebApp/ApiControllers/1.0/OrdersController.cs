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
using Order = Domain.Order;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }
        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = (await _bll.Orders.AllAsync())
                .Select(bllEntity => new Order()
                {
                    Id = bllEntity.Id,
                    Comment = bllEntity.Comment,
                }) ;
            
            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id, User.UserGuidId());

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order orderEditDTO)
        {
            if (id != orderEditDTO.Id)
            {
                return BadRequest();
            }

            var order = await _bll.Orders.FirstOrDefaultAsync(orderEditDTO.Id, User.UserGuidId());
            if (order == null)
            {
                return BadRequest();
            }

            order.Comment = order.Comment;

            _bll.Orders.Update(order);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Orders.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderCreate orderCreateDTO)
        {
            var order = new BLL.App.DTO.Order()
            {
                AppUserId = User.UserGuidId(),
                Comment = orderCreateDTO.Comment,
            };

            _bll.Orders.Add(order);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new {id = order.Id}, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(Guid id)
        {
            var order = await _bll.Orders 
                .FirstOrDefaultAsync(id, User.UserGuidId());
            if (order == null)
            {
                return NotFound();
            }

            _bll.Orders.Remove(order);
            await _bll.SaveChangesAsync();

            return Ok(order);
        }
    }
}
