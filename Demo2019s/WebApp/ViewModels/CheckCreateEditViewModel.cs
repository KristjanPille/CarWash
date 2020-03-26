using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class CheckCreateEditViewModel
    {
        public Check Check { get; set; } = default!;
        
        public SelectList? PersonSelectList { get; set; }
        public SelectList? WashSelectList { get; set; }
    }
}