using System;
using System.Threading.Tasks;
using ELearning.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ELearning.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ELearning.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
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
            if (ModelState.IsValid)
            {
                UniversityUser universityUser = new UniversityUser();
                universityUser.Id = Guid.NewGuid();
                universityUser.Firstname = model.FirstName;
                universityUser.Lastname = model.LastName;
                universityUser.email = model.Email;
                universityUser.Password = model.Password;
                universityUser.Type = "student";
                _context.Add(universityUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "UniversityUsers");

            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        
    }
}
