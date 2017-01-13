using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {

            return View();
        }

        public IActionResult Learn()
        {

            return View();
        }

<<<<<<< HEAD
=======
        public IActionResult Downloads()
        { 
            return View();
        }

        public IActionResult Topics()
        {
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");
            ViewBag.Type = HttpContext.Session.GetString("Type");
            return View();
        }
>>>>>>> 52ad092028b9cee0d916780bec9033a2a457465b

        public IActionResult FastQuestionProf()
        {

            return View();
        }

        public IActionResult FastQuestionResponse()
        {

            return View();
        }

        public IActionResult FastQuestionStud()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
