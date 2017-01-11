using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELearning.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ELearning.Controllers
{
    public class DownloadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DownloadsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<ActionResult> Downloads()
        {
            return View(await _context.Technologies.ToListAsync());
        }

    }
}
