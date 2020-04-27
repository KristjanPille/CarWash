using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using Campaign = Domain.Campaign;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CampaignsController : ControllerBase
    {
       
        private readonly IAppBLL _bll;

        public CampaignsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Campaigns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campaign>>> GetCampaigns()
        {
            var campaigns = (await _bll.Campaigns.AllAsync())
                .Select(bllEntity => new Campaign()
                {
                    Id = bllEntity.Id,
                    NameOfCampaign = bllEntity.NameOfCampaign,
                }) ;
            
            return Ok(campaigns);
        }

        // GET: api/Campaigns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Campaign>> GetCampaign(Guid id)
        {
            var campaign = await _bll.Campaigns.FirstOrDefaultAsync(id, User.UserGuidId());

            if (campaign == null)
            {
                return NotFound();
            }

            return Ok(campaign);
        }

        // PUT: api/Campaigns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampaign(Guid id, CampaignEdit campaignEditDTO)
        {
            if (id != campaignEditDTO.Id)
            {
                return BadRequest();
            }

            var campaign = await _bll.Campaigns.FirstOrDefaultAsync(campaignEditDTO.Id, User.UserGuidId());
            if (campaign == null)
            {
                return BadRequest();
            }

            campaign.NameOfCampaign = campaignEditDTO.NameOfCampaign;

            _bll.Campaigns.Update(campaign);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Campaigns.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();

        }

        // POST: api/Campaigns
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Campaign>> PostCampaign(CampaignCreate campaignCreateDTO)
        {
            var campaign = new BLL.App.DTO.Campaign()
            {
                AppUserId = User.UserGuidId(),
                NameOfCampaign = campaignCreateDTO.NameOfCampaign,
            };

            _bll.Campaigns.Add(campaign);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCampaign", new {id = campaign.Id}, campaign);

        }

        // DELETE: api/Campaigns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Campaign>> DeleteCampaign(Guid id)
        {
            var campaign = await _bll.Campaigns.FirstOrDefaultAsync(id, User.UserGuidId());
            if (campaign == null)
            {
                return NotFound();
            }

            _bll.Campaigns.Remove(campaign);
            await _bll.SaveChangesAsync();

            return Ok(campaign);
        }
    }
}
