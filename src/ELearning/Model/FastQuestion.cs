using System;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Model
{
    public class FastQuestion
    {
        public Guid Id { get; set; }
        
        [Required]
        public string QuestionText { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int Active { get; set; }
    }
}
