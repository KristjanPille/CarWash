using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class DiscountCreateEditViewModel
    {
        public Discount Discount { get; set; } = default!;
        
        public SelectList? CheckSelectList { get; set; }
        public SelectList? WashSelectList { get; set; }
    }
}