using Domain;
using Service = DAL.App.DTO.Service;

namespace WebApp.ViewModels
{
    public class ServiceCreateEditViewModel
    {
        public Service Service { get; set; } = default!;
    }
}