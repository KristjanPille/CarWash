using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class PaymentCreateEditViewModel
    {
        public Payment Payment { get; set; } = default!;
        
        public SelectList? CheckSelectList  { get; set; }
        public SelectList? PersonSelectList { get; set; }
        public SelectList? PaymentMethodSelectList { get; set; }
    }
}