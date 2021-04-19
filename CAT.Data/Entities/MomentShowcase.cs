using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Data.Entities
{
    public class MomentShowcase
    {
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(Moment))]
        public int MomentId { get; set; }
        public virtual Moment Moment { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Showcase))]
        public int ShowcaseId { get; set; }
        public virtual Showcase Showcase { get; set; }
    }
}
