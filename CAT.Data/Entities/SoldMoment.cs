using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Data.Entities
{
    public class SoldMoment
    {
        [Key]
        public int SoldMomentId { get; set; }
        
        [ForeignKey(nameof(Moment))]
        public int MomentId { get; set; }
        public virtual Moment Moment { get; set; }

        [Required]
        public decimal SoldForAmount { get; set; }
    }
}
