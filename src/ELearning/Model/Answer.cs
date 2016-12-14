using System;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Model
{
    public class Answer
    {
        public Guid Id { get; set; }
        [Required, Display(Name = "Question Id")]
        public Guid QuestionId { get; set; }
        [Required, Display(Name = "Answer")]
        public string AnswerText { get; set; }
        public bool Correct { get; set; }
    }
}
