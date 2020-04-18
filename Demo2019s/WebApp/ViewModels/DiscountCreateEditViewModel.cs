using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Discount = DAL.App.DTO.Discount;

namespace WebApp.ViewModels
{
    public class DiscountCreateEditViewModel
    {
        public Discount Discount { get; set; } = default!;
        
        public SelectList? CheckSelectList { get; set; }
        public SelectList? WashSelectList { get; set; }
    }
}