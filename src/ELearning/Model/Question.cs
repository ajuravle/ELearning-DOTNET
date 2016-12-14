using System;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Model
{
    public class Question
    {
        public Guid Id { get; set; }
        [Required, Display(Name = "Topic Id")]
        public Guid IdTopic { get; set; }
        [Required, Display(Name = "Question")]
        public string QuestionText { get; set; }
    }
}
