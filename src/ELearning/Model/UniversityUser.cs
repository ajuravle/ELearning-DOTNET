using System;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Model
{
    public class UniversityUser
    {
        public Guid Id { get; set; }
        [Required, Display(Name = "First Name")]
        public string Firstname { get; set; }
        [Required, Display(Name = "Last Name")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Password is required"), DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Must be between 7 and 255 characters", MinimumLength = 7)]
        public string Password { get; set; }
        [Required,Display(Name ="Type of user"), RegularExpression("(student|professor|admin)$", ErrorMessage = "Type of user must be student, professor or admin")]
        public string Type { get; set; }
        [Required, EmailAddress, Display(Name ="E-mail address")]
        public string email { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
