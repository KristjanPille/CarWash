using System.Threading.Tasks;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AppUser appUser;
        // GET
        public async Task<IActionResult> Index()
        {
            var usersInRole = appUser.Email;

            return View();
        }
        
    }
}