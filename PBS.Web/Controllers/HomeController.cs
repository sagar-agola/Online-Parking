using Microsoft.AspNetCore.Mvc;
using PBS.Web.Models;

namespace PBS.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index ()
        {
            return View ();
        }

        public IActionResult Privacy ()
        {
            return View ();
        }

        public IActionResult Error (ErrorViewModel model)
        {
            return View (model);
        }
    }
}
