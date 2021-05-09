using CAT.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.Showcase_Models
{
    public class ShowcaseCreate
    {
        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        [Display(Name = "Showcase")]
        [MaxLength(50)]
        public string ShowcaseName { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MaxLength(100)]
        public string ShowcaseDescription { get; set; }
    }
}
