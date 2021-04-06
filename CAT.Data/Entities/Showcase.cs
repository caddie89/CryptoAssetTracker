using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Data.Entities
{
    public class Showcase
    {
        [Key]
        public int ShowcaseId { get; set; }

        [ForeignKey(nameof(Moment))]
        public int? MomentId { get; set; }
        public virtual Moment Moment { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string ShowcaseName { get; set; }

        [Required]
        public string ShowcaseDescription { get; set; }
    }
}
