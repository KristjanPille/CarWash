using Domain;
using ModelMark = DAL.App.DTO.ModelMark;

namespace WebApp.ViewModels
{
    public class ModelMarkCreateEditViewModel
    {
        public ModelMark ModelMark { get; set; } = default!;
    }
}