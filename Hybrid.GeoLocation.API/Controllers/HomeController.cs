using Microsoft.AspNetCore.Mvc;

namespace Hybrid.GeoLocation.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}