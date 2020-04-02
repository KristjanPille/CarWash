using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Client.viewmodels
{
    public class PersonCreateEditViewModel
    {
        public Person Person { get; set; } = default!;
        public SelectList? PersonTypeSelectList { get; set; }
    }
}