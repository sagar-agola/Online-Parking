using Microsoft.AspNetCore.Mvc;

namespace PBS.Web.Areas.Admin.Controllers
{
    [Area ("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index ()
        {
            return View ();
        }
    }
}