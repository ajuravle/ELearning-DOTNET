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
    public class UniversityUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UniversityUsersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: UniversityUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.UniversityUsers.ToListAsync());
        }

        // GET: UniversityUsers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universityUser = await _context.UniversityUsers.SingleOrDefaultAsync(m => m.Id == id);
            if (universityUser == null)
            {
                return NotFound();
            }

            return View(universityUser);
        }

        // GET: UniversityUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UniversityUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,Password,Type,email")] UniversityUser universityUser)
        {
            if (ModelState.IsValid)
            {
                universityUser.Id = Guid.NewGuid();
                _context.Add(universityUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(universityUser);
        }

        // GET: UniversityUsers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universityUser = await _context.UniversityUsers.SingleOrDefaultAsync(m => m.Id == id);
            if (universityUser == null)
            {
                return NotFound();
            }
            return View(universityUser);
        }

        // POST: UniversityUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Firstname,Lastname,Password,Type,email")] UniversityUser universityUser)
        {
            if (id != universityUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(universityUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniversityUserExists(universityUser.Id))
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
            return View(universityUser);
        }

        // GET: UniversityUsers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universityUser = await _context.UniversityUsers.SingleOrDefaultAsync(m => m.Id == id);
            if (universityUser == null)
            {
                return NotFound();
            }

            return View(universityUser);
        }

        // POST: UniversityUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var universityUser = await _context.UniversityUsers.SingleOrDefaultAsync(m => m.Id == id);
            _context.UniversityUsers.Remove(universityUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UniversityUserExists(Guid id)
        {
            return _context.UniversityUsers.Any(e => e.Id == id);
        }
    }
}
