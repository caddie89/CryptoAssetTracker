using CAT.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.Showcase_Models
{
    public class ShowcaseIndex
    {
        public int ShowcaseId { get; set; }

        [Display(Name = "Showcase")]
        public string ShowcaseName { get; set; }

        [Display(Name = "Description")]
        public string ShowcaseDescription { get; set; }

        public virtual ICollection<Moment> Moments { get; set; } = new List<Moment>();

    }
}
