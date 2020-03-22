using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class CampaignCreateEditViewModel
    {
        public Campaign Campaign { get; set; } = default!;
        
        public SelectList? ServiceSelectList { get; set; }
    }
}