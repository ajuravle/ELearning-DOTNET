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
    public class FastQuestionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FastQuestionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
    public async Task<FastQuestion> Create([FromBody] FastQuestion fastQuestion)
        {
            if (ModelState.IsValid)
            {
                fastQuestion.Id = Guid.NewGuid();
                _context.Add(fastQuestion);
                await _context.SaveChangesAsync();
            }
            return fastQuestion;
        }

    }
}
