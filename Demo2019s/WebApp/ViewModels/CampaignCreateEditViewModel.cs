using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Campaign = DAL.App.DTO.Campaign;

namespace WebApp.ViewModels
{
    public class CampaignCreateEditViewModel
    {
        public Campaign Campaign { get; set; } = default!;
        
        public SelectList? ServiceSelectList { get; set; }
    }
}