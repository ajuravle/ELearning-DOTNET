using System;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Model
{
    public class Topic
    {
        public Guid Id { get; set; }

        [Required, Display(Name = "Topic's name")]
        public string TechnologyName { get; set; }

        [Required, Display(Name = "Technology's Id")]
        public Guid IdTechnology { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string urmImage { get; set; }
    }
}
