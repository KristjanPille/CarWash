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
    /// campaigns Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CampaignsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CampaignMapper _mapper = new CampaignMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public CampaignsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/campaigns
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Campaign>>> Getcampaigns()
        {
            return Ok((await _bll.Campaigns.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/campaigns/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<Campaign>> Getcampaign(Guid id)
        {
            var campaign= await _bll.Campaigns.FirstOrDefaultAsync(id);

            if (campaign== null)
            {
                return NotFound(new V1DTO.MessageDTO("campaign not found"));
            }

            return Ok(_mapper.Map(campaign));
        }

        // PUT: api/campaigns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> Putcampaign(Guid id, V1DTO.Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and campaign.id do not match"));
            }

            await _bll.Campaigns.UpdateAsync(_mapper.Map(campaign));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/campaigns
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Campaign))]
        public async Task<ActionResult<Campaign>> Postcampaign(V1DTO.Campaign campaign)
        {
            var bllEntity = _mapper.Map(campaign);
            _bll.Campaigns.Add(bllEntity);
            await _bll.SaveChangesAsync();
            campaign.Id = bllEntity.Id;

            return CreatedAtAction("Getcampaign",
                new {id = campaign.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                campaign);
        }

        // DELETE: api/campaigns/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<Campaign>> Deletecampaign(Guid id)
        {
            var campaign= await _bll.Campaigns.FirstOrDefaultAsync(id);
            if (campaign== null)
            {
                return NotFound(new V1DTO.MessageDTO("campaign not found"));
            }

            await _bll.Campaigns.RemoveAsync(campaign);
            await _bll.SaveChangesAsync();

            return Ok(campaign);
        }
    }
}
