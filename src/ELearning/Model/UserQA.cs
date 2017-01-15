using System;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Model
{
    public class UserQA
    {
        public Guid Id { get; set; }
        
        [Required]
        public Guid IdUser { get; set; }

        [Required]
        public Guid IdQA { get; set; }


    }
}
