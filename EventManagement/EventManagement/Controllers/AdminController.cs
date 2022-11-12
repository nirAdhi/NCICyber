using EventManagement.Utility;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Controllers
{
    [AdminAuthorization]
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
