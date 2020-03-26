using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class CarCreateEditViewModel
    {
        public Car Car { get; set; } = default!;
        
        public SelectList? CarTypeSelectList { get; set; }
        public SelectList? PersonSelectList { get; set; }
        public SelectList? CampaignSelectList { get; set; }

        public SelectList? ModelMarkSelectList { get; set; }
        public SelectList? IsInWashSelectList { get; set; }
        
    }
}