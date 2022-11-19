using EventManagement.Utility;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Models;
using EventManagement.DataDB;
using System.Net.Http;
using System.Text.Json;

namespace EventManagement.Controllers
{
    [AdminAuthorization]
    public class AdminController : Controller
    {
        public IActionResult Dashboard(DashboardModel dmodel)
        {
            using ( EventManagementContext context = new EventManagementContext())
            {
                dmodel.hackathonCount= context.EventRegistrations.Where(x=> (x.Event1Id==1 || x.Event2Id==1 || x.Event3Id==1)).Count();
                dmodel.bugCount = context.EventRegistrations.Where(x => (x.Event1Id == 2 || x.Event2Id == 2 || x.Event3Id == 2)).Count();
                dmodel.cyberCount = context.EventRegistrations.Where(x => (x.Event1Id == 3 || x.Event2Id == 3 || x.Event3Id == 3)).Count();
                dmodel.userCount=context.Users.Where(x=> x.RoleId==2).Count();
            }
           
            

            return View(dmodel);
        }
        [HttpGet]
        public IActionResult EventDetails(int eventid)
        {
            using (EventManagementContext context = new EventManagementContext())
            {
                string eventName = "";
                switch (eventid)
                {
                    case 1:
                        eventName = "Hackathon";
                        break;
                    case 2:
                        eventName = "Bug Hunter";
                        break;
                    case 3:
                        eventName = "Cyber League";
                        break;
                }
                IFormatProvider provider;
                
                var eventList = from e in context.EventRegistrations
                                join u in context.Users on e.UserId equals u.Id
                                where e.Event1Id == eventid || e.Event2Id == eventid || e.Event3Id == eventid
                                select new
                                {
                                    name = u.FirstName + " " + u.LastName,
                                    email = u.Email,
                                    eventName = eventName,
                                    date = e.CreateDate.Value.ToString("mm/dd/yyyy")
                                };
                   

                return new JsonResult(eventList.ToList());
            }
        }
    }
}
