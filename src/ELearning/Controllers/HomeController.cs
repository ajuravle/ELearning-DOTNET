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
