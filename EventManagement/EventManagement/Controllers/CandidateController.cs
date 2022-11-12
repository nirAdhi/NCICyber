using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Controllers
{
    public class CandidateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
