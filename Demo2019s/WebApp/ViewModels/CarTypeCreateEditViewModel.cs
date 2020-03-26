using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class CarTypeCreateEditViewModel
    {
        public CarType CarType { get; set; } = default!;
    }
}