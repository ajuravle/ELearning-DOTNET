using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearning.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ELearning.Model;
using ELearning.Services;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ELearning.Controllers
{
    public class AccountController : Controller
    {
        private readonly EmailService _emailService;
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
            _emailService = new EmailService();
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var account = _context.UniversityUsers.FirstOrDefault(x => x.email == model.Email);

            if (ModelState.IsValid && account == null)
            {
                UniversityUser universityUser = new UniversityUser();
                universityUser.Id = Guid.NewGuid();
                universityUser.Firstname = model.FirstName;
                universityUser.Lastname = model.LastName;
                universityUser.email = model.Email;
                universityUser.Password = model.Password;
                universityUser.Type = "student";
                universityUser.Avtive = false;
                _context.Add(universityUser);
                await _context.SaveChangesAsync();

                _emailService.SendMail(model.Email, "http://"+HttpContext.Request.GetUri().Authority+"/UniversityUsers/ActivateUser/"+ universityUser.Id);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if( account != null)
                
                    ModelState.AddModelError("Email", "Email already exist ");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        { 
            if (ModelState.IsValid)
            {

                var user = await _context.UniversityUsers.SingleOrDefaultAsync(m => m.email == model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Credentials", "Incorect credentials");
                    return View();
                }
                else if (user.Password == model.Password)
                {
                    if (!user.Avtive)
                    {
                        ModelState.AddModelError("Active", "Please activate your account first");
                        return View();
                    }
                    HttpContext.Session.SetString("ID", user.Id.ToString());
                    HttpContext.Session.SetString("Email", user.email);
                    HttpContext.Session.SetString("FirstName", user.Firstname);
                    HttpContext.Session.SetString("LastName", user.Lastname);
                    HttpContext.Session.SetString("Type", user.Type);
                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError("Credentials", "Incorect credentials");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet, ActionName("GetAll")]
        public IActionResult GetAll()
        {
            List<string> sessionVar = new List<string>();
            sessionVar.Add(HttpContext.Session.GetString("Email"));
            sessionVar.Add(HttpContext.Session.GetString("FirstName"));
            sessionVar.Add(HttpContext.Session.GetString("LastName"));
            sessionVar.Add(HttpContext.Session.GetString("Type"));
            sessionVar.Add(HttpContext.Session.GetString("ID"));
            return new ObjectResult(sessionVar);
        }

        // GET: /Account/Logout
        [AllowAnonymous]
        public ActionResult Logout()
        {
            HttpContext.Session.SetString("Email", "");
            HttpContext.Session.SetString("FirstName", "");
            HttpContext.Session.SetString("LastName", "");
            HttpContext.Session.SetString("Type", "");
            HttpContext.Session.SetString("ID", "");
            return RedirectToAction("Login", "Account");
        }

        // GET: Account/UserPage
        public async Task<IActionResult> UserPage()
        {
            return View();
        }

        // POST: /Account/UserPage
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserPage(UserPageViewModel model)
        {
            UniversityUser universityUser = await _context.UniversityUsers.FirstOrDefaultAsync(
                x => x.email == HttpContext.Session.GetString("Email"));
            if (ModelState.IsValid && universityUser != null)
            {
                universityUser.Firstname = model.FirstName;
                universityUser.Lastname = model.LastName;
                universityUser.Password = model.Password;
                _context.Update(universityUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("UserPage", "Account");
        }
    }

}

