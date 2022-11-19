using Microsoft.AspNetCore.Mvc;
using EventManagement.Models;
using EventManagement.Utility;
using EventManagement.DataDB;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Controllers
{
   
   [CandidateAuthorization]
    public class CandidateController : Controller
    {
        public EventManagementContext context = new EventManagementContext();
        public IActionResult Index()
        {
            Guid userid = new Guid();
            Guid.TryParse(HttpContext.Session.GetString("id"), out userid);
            if (context.EventRegistrations.Any(u => u.UserId == userid))
            {
                ViewBag.status = 1;
                ViewBag.msg = "Thank you for your successfull registration with us.we will update you further details.";
            }
            else
            {
                ViewBag.status = 0;
                ViewBag.msg = "You are not participate any event .To participate click here ";
            }
            return View();
        }
        [HttpGet]
        public IActionResult EventRegistration()
        {
            EventReg reg = new EventReg();

            ViewBag.status = 999;
            Guid userid = new Guid();
            Guid.TryParse(HttpContext.Session.GetString("id"), out userid);
            if (context.EventRegistrations.Any(u => u.UserId == userid))
            {
                ViewBag.status = 3;
                ViewBag.msg = "You are already registered";
                return View(reg);
            }
            return View(reg);
        }

        [HttpPost]
        public IActionResult EventRegistration(EventReg reg)
        {
            if (ModelState.IsValid)
            {
                using (context)
                {

                    Guid userid = new Guid();
                    Guid.TryParse(HttpContext.Session.GetString("id"), out userid);
                    if (context.EventRegistrations.Any(u => u.UserId == userid))
                    {
                        ViewBag.status = 3;
                        ViewBag.msg = "You are already registered";
                        return View(reg);
                    }
                    var eventreg = new EventRegistration
                    {
                        UserId = userid,
                        Event1Id = reg.Event1Id,
                        Event2Id = reg.Event2Id,
                        Event3Id = reg.Event3Id,
                        TotalAmount = reg.TotalAmount,
                        CreateDate=DateTime.Now
                    };
                    var regId = context.EventRegistrations.Add(eventreg);
                    var status = context.SaveChanges();
                    if (status > 0)
                    {
                        ViewBag.status = 1;
                        ViewBag.msg = "Event register Successfully";
                    }
                    else
                    {
                        ViewBag.status = 0;
                        ViewBag.msg = "some error occured";
                    }

                }
            }
            return View(reg);
        }
    }
}
