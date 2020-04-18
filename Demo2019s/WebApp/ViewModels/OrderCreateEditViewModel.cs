using Domain;
using Order = DAL.App.DTO.Order;

namespace WebApp.ViewModels
{
    public class OrderCreateEditViewModel
    {
        public Order Order { get; set; } = default!;
    }
}