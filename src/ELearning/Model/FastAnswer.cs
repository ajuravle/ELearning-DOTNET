using System;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Model
{
    public class FastAnswer
    {
        public Guid Id { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}
