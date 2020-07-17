using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PublicApi.DTO.v1;
using Campaign = BLL.App.DTO.Campaign;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Culture info
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CulturesController : ControllerBase
    {
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="localizationOptions"></param>
        public CulturesController(IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _localizationOptions = localizationOptions;
        }

        /// <summary>
        /// Get the list of supported cultures
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CultureDTO>))]
        public ActionResult<IEnumerable<CultureDTO>> GetCultures()
        {
            var result = _localizationOptions.Value.SupportedUICultures
                .Select(c => new CultureDTO()
                {
                    Code = c.Name,
                    Name = c.NativeName,
                }).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Get the resource strings and keys
        /// </summary>
        /// <returns></returns>
        [HttpGet("resources")]
        public ActionResult<IEnumerable<string>> GetResources()
        {
            var res = new List<CultureDTO>();
            var resourceSet =
                Resources.Views.Shared._Layout.ResourceManager
                    .GetResourceSet(Thread.CurrentThread.CurrentUICulture,
                        true, true);
                        
            if (resourceSet == null)
            {
                return Ok(res);
            }

            foreach (DictionaryEntry? entry in resourceSet)
            {
                if (entry==null) continue;
                res.Add(new CultureDTO() {Name = entry.Value.Value!.ToString()!, Code = entry.Value.Key.ToString()!});
            }
            return Ok(res);
        }
        
        /// <summary>
        /// Get the resource strings and keys
        /// </summary>
        /// <returns></returns>
        [HttpGet("entityResources/{entity}")]
        public ActionResult<IEnumerable<string>> GetEntityResources(string entity)
        {
            ResourceSet? resourceSet = null;
            var res = new List<CultureDTO>();
            resourceSet = entity switch
            {
                "Campaign" => Resources.BLL.App.DTO.Campaign.ResourceManager.GetResourceSet(
                    Thread.CurrentThread.CurrentUICulture, true, true),
                "Car" => Resources.BLL.App.DTO.Car.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture,
                    true, true),
                "Payment" => Resources.BLL.App.DTO.Payment.ResourceManager.GetResourceSet(
                    Thread.CurrentThread.CurrentUICulture, true, true),
                "PaymentMethod" => Resources.BLL.App.DTO.PaymentMethod.ResourceManager.GetResourceSet(
                    Thread.CurrentThread.CurrentUICulture, true, true),
                "Service" => Resources.BLL.App.DTO.Service.ResourceManager.GetResourceSet(
                    Thread.CurrentThread.CurrentUICulture, true, true),
                "Order" => Resources.BLL.App.DTO.Order.ResourceManager.GetResourceSet(
                    Thread.CurrentThread.CurrentUICulture, true, true),
                "ModelMark" => Resources.BLL.App.DTO.ModelMark.ResourceManager.GetResourceSet(
                    Thread.CurrentThread.CurrentUICulture, true, true),
                _ => resourceSet
            };
            if (resourceSet == null)
            {
                return Ok(res);
            }

            foreach (DictionaryEntry? entry in resourceSet)
            {
                if (entry==null) continue;
                res.Add(new CultureDTO() {Name = entry.Value.Value!.ToString()!, Code = entry.Value.Key.ToString()!});
            }
            return Ok(res);
        }
    }
}