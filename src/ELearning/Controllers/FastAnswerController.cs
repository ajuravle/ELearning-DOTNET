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
    public class FastAnswerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FastAnswerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<FastAnswer> Create([FromBody] FastAnswer fastAnswer)
        {
            if (ModelState.IsValid)
            {

                fastAnswer.Id = Guid.NewGuid();
                _context.Add(fastAnswer);
                await _context.SaveChangesAsync();
            }
            return fastAnswer;
        }

    }
}
