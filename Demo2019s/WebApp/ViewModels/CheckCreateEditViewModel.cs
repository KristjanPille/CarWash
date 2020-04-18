using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Check = DAL.App.DTO.Check;

namespace WebApp.ViewModels
{
    public class CheckCreateEditViewModel
    {
        public Check Check { get; set; } = default!;
        
        public SelectList? PersonSelectList { get; set; }
        public SelectList? WashSelectList { get; set; }
    }
}