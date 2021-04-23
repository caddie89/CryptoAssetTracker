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
        public int? MomentId { get; set; }
        public virtual Moment Moment { get; set; }

        [ForeignKey(nameof(Player))]
        public int? PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string MomentCategory { get; set; }

        [Required]
        public DateTimeOffset DateOfMoment { get; set; }

        [Required]
        public string MomentSet { get; set; }

        [Required]
        public int MomentSeries { get; set; }

        [Required]
        public int MomentSerialNumber { get; set; }

        [Required]
        public int MomentCirculatingCount { get; set; }

        [Required]
        public Tier MomentTier { get; set; }

        [Required]
        public Mint MomentMint { get; set; }

        [Required]
        public bool PurchasedInPack { get; set; }

        public decimal? PackPrice { get; set; }

        public decimal? AmountInPack { get; set; }

        [Required]
        public decimal IndividualMomentPrice { get; set; }

        [Required]
        public decimal SoldForAmount { get; set; }
    }
}
