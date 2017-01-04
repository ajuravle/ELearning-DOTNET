using System;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Model
{
    public class Material
    {
        public Guid Id { get; set; }

        [Required, Display(Name = "Topic's Id")]
        public Guid IdTopic { get; set; }

        [Required, Display(Name = "Material URL")]
        public string UrlMaterial { get; set; }
    }
}
