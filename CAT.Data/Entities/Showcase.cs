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

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string ShowcaseName { get; set; }

        [Required]
        public string ShowcaseDescription { get; set; }

        public virtual ICollection<Moment> Moments { get; set; } = new List<Moment>();

        public virtual ICollection<Moment> SoldMoments { get; set; } = new List<Moment>();
    }
}
