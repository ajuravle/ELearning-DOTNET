using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELearning.Data;
using ELearning.Model;

namespace ELearning.Controllers
{
    public class TechnologiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TechnologiesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Technologies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Technologies.ToListAsync());
        }

        // GET: Technologies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technology = await _context.Technologies.SingleOrDefaultAsync(m => m.Id == id);
            if (technology == null)
            {
                return NotFound();
            }

            return View(technology);
        }

        // GET: Technologies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Technologies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdProfessor,Name")] Technology technology)
        {
            if (ModelState.IsValid)
            {
                technology.Id = Guid.NewGuid();
                _context.Add(technology);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(technology);
        }

        // GET: Technologies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technology = await _context.Technologies.SingleOrDefaultAsync(m => m.Id == id);
            if (technology == null)
            {
                return NotFound();
            }
            return View(technology);
        }

        // POST: Technologies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,IdProfessor,Name")] Technology technology)
        {
            if (id != technology.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technology);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechnologyExists(technology.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(technology);
        }

        // GET: Technologies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technology = await _context.Technologies.SingleOrDefaultAsync(m => m.Id == id);
            if (technology == null)
            {
                return NotFound();
            }

            return View(technology);
        }

        // POST: Technologies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var technology = await _context.Technologies.SingleOrDefaultAsync(m => m.Id == id);
            _context.Technologies.Remove(technology);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TechnologyExists(Guid id)
        {
            return _context.Technologies.Any(e => e.Id == id);
        }

        [HttpGet, ActionName("GetAll")]
        public IActionResult GetAll()
        {
            var technologies = _context.Technologies.AsEnumerable();
            return new ObjectResult(technologies);
        }
    }
}
