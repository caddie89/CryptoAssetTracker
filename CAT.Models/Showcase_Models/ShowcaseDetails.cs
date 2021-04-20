using CAT.Models.Moment_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.Showcase_Models
{
    public class ShowcaseDetails
    {
        [Display(Name = "Showcase ID")]
        public int ShowcaseId { get; set; }

        [Display(Name = "Showcase")]
        public string ShowcaseName { get; set; }

        [Display(Name = "Description")]
        public string ShowcaseDescription{ get; set; }

        public virtual ICollection<MomentIndex> Moments { get; set; }
    }
}
