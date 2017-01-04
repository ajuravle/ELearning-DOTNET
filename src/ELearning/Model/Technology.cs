using System;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Model
{
    public class Technology
    {
        public Guid Id { get; set; }

        [Display(Name = "Professor Id")]
        public Guid IdProfessor { get; set; }

        [Required, Display(Name = "Technology's name")]
        public string Name { get; set; }

        [Required, Display(Name = "Image URL")]
        public string UrlImage { get; set; }

    }
}
