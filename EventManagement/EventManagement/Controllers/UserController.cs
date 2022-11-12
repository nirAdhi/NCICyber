using Microsoft.AspNetCore.Mvc;
using EventManagement.Models;
using EventManagement.DataDB;
using Microsoft.AspNetCore.Identity;
using EventManagement.Utility;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authentication;

namespace EventManagement.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            UserModel userModel = new UserModel();
            return View(userModel);
        }
        [HttpPost]
        public IActionResult Index(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                EventManagementContext dbContext = new EventManagementContext();
                if (dbContext.Users.Any(u => u.Email == userModel.email))
                {
                    ViewBag.status = 0;
                    ViewBag.msg = "This E-mail already exists";
                    return View(userModel);
                }
                Crypto.CreatePasswordHash(userModel.password, out string passwordHash, out string passwordSalt);
                var user = new User
                {

                    FirstName = userModel.firstName,
                    LastName = userModel.lastName,
                    Email = userModel.email,
                    Gender = userModel.gender,
                    Mobile = userModel.mobile,
                    RoleId = 2,     //2-Candidate,1-Admin
                    CreateDate = DateTime.Now,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    VerificationToken = Crypto.CreateRandomToken()
                };
                var status = dbContext.Users.Add(user);
                int res=dbContext.SaveChanges();
                dbContext.Dispose();
                if (res > 0)
                {
                    return RedirectToAction("Index", "Candidate");
                }
            }
           
           
            return View(userModel);
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginModel login = new LoginModel();
            return View(login);
        }
        [HttpPost]
        public IActionResult Login(LoginModel login )
        {
            using (EventManagementContext context = new EventManagementContext())
            {
                
                var objUser = context.Users.Where(u => u.Email == login.email).FirstOrDefault();
                if (objUser != null)
                {
                    var status = Crypto.VerifyPasswordHash(login.password, objUser.PasswordHash, objUser.PasswordSalt);
                    if (status)
                    {
                        if (objUser.RoleId == 1)
                        {
                            HttpContext.Session.SetString("username", objUser.FirstName);
                            HttpContext.Session.SetString("email", objUser.Email);
                            HttpContext.Session.SetString("role", "admin");
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        else
                        {
                            HttpContext.Session.SetString("username", objUser.FirstName);
                            HttpContext.Session.SetString("email", objUser.Email);
                            HttpContext.Session.SetString("role", "candidate");
                            return RedirectToAction("Index", "Candidate");
                        }
                    }
                }
            }
           
            return View(login);
        }

        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            var storedcookies=HttpContext.Request.Cookies.Keys;
            foreach(var cookie in storedcookies)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Login", "User");
        }
    }
}
