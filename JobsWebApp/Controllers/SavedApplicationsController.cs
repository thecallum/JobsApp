using Microsoft.AspNetCore.Mvc;

namespace JobsWebApp.Controllers
{
    public class SavedApplicationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}