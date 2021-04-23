using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CAT.Models.MomentShowcase_Models
{
    public class MomentShowcaseCreate
    {
        [Required]
        public int MomentId { get; set; }

        [Required]
        public int ShowcaseId { get; set; }

        public int[] MomentIds { get; set; }
    }
}
