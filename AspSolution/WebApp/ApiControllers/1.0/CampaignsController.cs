using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using Campaign = Domain.App.Campaign;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
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
        /// <summary>
        /// Get all campaigns
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Campaign>>> GetCampaigns()
        {
            return Ok((await _bll.Campaigns.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/campaigns/5
        /// <summary>
        /// Get Specific Campaign
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<PublicApi.DTO.v1.Campaign>> GetCampaign(Guid id)
        {
            var campaign= await _bll.Campaigns.FirstOrDefaultAsync(id);

            if (campaign== null)
            {
                return NotFound(new V1DTO.MessageDTO("Campaign not found"));
            }

            return Ok(_mapper.Map(campaign));
        }

        // PUT: api/campaigns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update Camapign, only for admins
        /// </summary>
        /// <param name="id"></param>
        /// <param name="campaign"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutCampaign(Guid id, V1DTO.Campaign campaign)
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
        /// <summary>
        /// Create new campaign. only for admins
        /// </summary>
        /// <param name="campaign"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Campaign))]
        public async Task<ActionResult<Campaign>> PostCampaign(V1DTO.Campaign campaign)
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
        /// <summary>
        /// Delete Campaign, only for admins
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<Campaign>> DeleteCampaign(Guid id)
        {
            var campaign= await _bll.Campaigns.FirstOrDefaultAsync(id);
            if (campaign== null)
            {
                return NotFound(new V1DTO.MessageDTO("campaign not found"));
            }

            await _bll.Campaigns.RemoveAsync(campaign);
            await _bll.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
