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

        [HttpGet, ActionName("GetByQuestionID")]
        public IActionResult GetByQuestionId(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = _context.FastAnswer.Where(c => c.QuestionId.Equals(id));

            if (answers == null)
            {
                return NotFound();
            }

            return new ObjectResult(answers);
        }

        [HttpPost]
        public async Task<UserQA> AddUserResponse([FromBody] UserQA userQA)
        {
            if (ModelState.IsValid)
            {
                userQA.Id = Guid.NewGuid();
                _context.Add(userQA);
                await _context.SaveChangesAsync();
            }
            return userQA;
        }

        [HttpGet, ActionName("GetUserByQuestion")]
        public IActionResult GetUserByQuestion(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.UserQA.Where(c => c.IdQA.Equals(id)).AsEnumerable();

            return new ObjectResult(user);
        }

    }
}
