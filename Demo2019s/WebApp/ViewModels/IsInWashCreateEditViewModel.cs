using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using IsInWash = DAL.App.DTO.IsInWash;

namespace WebApp.ViewModels
{
    public class IsInWashCreateEditViewModel
    {
        public IsInWash IsInWash { get; set; } = default!;
        
        public SelectList? CarSelectList  { get; set; }
        public SelectList? PersonSelectList { get; set; }
        public SelectList? WashSelectList { get; set; }
    }
}