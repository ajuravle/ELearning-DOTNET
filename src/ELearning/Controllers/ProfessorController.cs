using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELearning.Data;
using Microsoft.EntityFrameworkCore;
using ELearning.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace ELearning.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfessorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateTechnology()
        {
            var professors = await _context.UniversityUsers.Where(u => u.Type == UserType.Professor && u.Active == true).ToListAsync();
            SelectList selectProfessors = null;

            if (professors != null)
            {
                selectProfessors = new SelectList(professors.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.Firstname} {p.Lastname}"
                }), "Value", "Text");
            }
            else
            {
                selectProfessors = new SelectList(new List<SelectListItem>());
            }

            ViewBag.Professors = selectProfessors;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTechnology([Bind("Id,IdProfessor,Name,UrlImage")] Technology technology)
        {
            if (ModelState.IsValid)
            {
                technology.Id = Guid.NewGuid();
                _context.Add(technology);
                await _context.SaveChangesAsync();
                return RedirectToAction("CreateTechnology");
            }

            return View(technology);
        }

        public async Task<IActionResult> CreateTopic()
        {
            string userId = HttpContext.Session.GetString("ID");

            Guid professorId = new Guid(userId);
            var technologies = await _context.Technologies
                .Where(t => t.IdProfessor == professorId)
                .ToListAsync();

            SelectList selectTechnologies = null;

            if (technologies != null)
            {
                selectTechnologies = new SelectList(technologies.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }), "Value", "Text");
            }
            else
            {
                selectTechnologies = new SelectList(new List<SelectListItem>());
            }

            ViewBag.Technologies = selectTechnologies;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTopic([Bind("IdTechnology,TopicName")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.Id = Guid.NewGuid();
                _context.Add(topic);
                await _context.SaveChangesAsync();
                return RedirectToAction("CreateTopic");
            }

            return View(topic);
        }

        public async Task<IActionResult> CreateMaterial()
        {
            string userId = HttpContext.Session.GetString("ID");

            Guid professorId = new Guid(userId);
            var technologies = await _context.Technologies
                .Where(t => t.IdProfessor == professorId)
                .ToListAsync();

            SelectList selectTechnologies = null;

            if (technologies != null)
            {
                selectTechnologies = new SelectList(technologies.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }), "Value", "Text");
            }
            else
            {
                selectTechnologies = new SelectList(new List<SelectListItem>());
            }

            ViewBag.Technologies = selectTechnologies;

            if (technologies.Any())
            {
                SelectList selectTopics = null;
                IEnumerable<Guid> technologiesIds = technologies.Select(t => t.Id);
                var topics = await _context.Topics.Where(t => technologiesIds.Contains(t.IdTechnology))
                    .ToListAsync();

                if (topics != null)
                {
                    selectTopics = new SelectList(topics.Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.TopicName
                    }), "Value", "Text");
                }
                else
                {
                    selectTopics = new SelectList(new List<SelectListItem>());
                }

                ViewBag.Topics = selectTopics;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMaterial([Bind("IdTopic,UrlMaterial")] Material material)
        {
            if (ModelState.IsValid)
            {
                material.Id = Guid.NewGuid();
                _context.Add(material);
                await _context.SaveChangesAsync();

                return RedirectToAction("CreateMaterial");
            }

            return View(material);
        }
    }
}