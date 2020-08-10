using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using Order = PublicApi.DTO.v1.Order;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{    /// <summary>
    /// Orders
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class OrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderMapper _mapper = new OrderMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }
        /// <summary>
        /// get all the Orders
        /// </summary>
        /// <returns>Array of Orders</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Order>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Order>>> GetOrders()
        {
            return Ok((await _bll.Orders.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Order
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns>Order object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Order))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id);

            if (order == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Order with id {id} not found"));
            }

            return Ok(_mapper.Map(order));
        }
        
        /// <summary>
        /// Update the Orders
        /// </summary>
        /// <param name="id">Session Id</param>
        /// <param name="order">Order object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutOrder(Guid id, V1DTO.Order order)
        {
            if (id != order.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and Order.id do not match"));
            }

            if (!await _bll.Orders.ExistsAsync(order.Id, User.UserId()))
            {
                return NotFound(new V1DTO.MessageDTO($"Current user does not have Order with this id {id}"));
            }

            order.AppUserId = User.UserId();
            await _bll.Orders.UpdateAsync(_mapper.Map(order));
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
        
        /// <summary>
        /// Post the new Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Order))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<Order>> PostOrder(V1DTO.Order order)
        {
            order.AppUserId = User.UserId();
            var bllEntity = _mapper.Map(order);
            
            _bll.Orders.Add(bllEntity);
            await _bll.SaveChangesAsync();
            order.Id = bllEntity.Id;

            return CreatedAtAction("GetOrder",
                new {id = order.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                order);
        }
        
        /// <summary>
        /// Delete the Order
        /// </summary>
        /// <param name="id">Order Id to delete.</param>
        /// <returns>Order just deleted</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Order))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<Order>> DeleteOrder(Guid id)
        {
            var userIdTKey = User.IsInRole("admin") ? null : (Guid?) User.UserId();

            var order =
                await _bll.Orders.FirstOrDefaultAsync(id, userIdTKey);
            
            if (order == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Order with id {id} not found!"));
            }

            await _bll.Orders.RemoveAsync(order, userIdTKey);
            await _bll.SaveChangesAsync();

            return Ok(order);
        }
    }
}
