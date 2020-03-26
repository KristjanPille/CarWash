using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class WashCreateEditViewModel
    {
        public Wash Wash { get; set; } = default!;
        
        public SelectList? OrderSelectList { get; set; }
        
    }
}