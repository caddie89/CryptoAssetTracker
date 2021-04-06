using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Data.Entities
{
    public class Moment
    {
        [Key]
        public int MomentId { get; set; }

        [ForeignKey(nameof(Player))]
        public int? PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string MomentCategory { get; set; }

        [Required]
        public string MomentSet { get; set; }

        [Required]
        public int MomentSerialNumber { get; set; }

        [Required]
        public Tier MomentTier { get; set; }

        [Required]
        public Mint MomentMint { get; set; }

        [Required]
        public bool PurchasedInPack { get; set; }

        public int AmountInPack { get; set; }

        [Required]
        public decimal PurchasePrice { get; set; }

        public decimal ActualPurchasePrice
        {
            get
            {
                decimal actualPrice = PurchasePrice / AmountInPack;
                return actualPrice = (decimal)System.Math.Round
                (actualPrice, 2);
            }
        }

        public virtual ICollection<Showcase> Showcases { get; set; } = new List<Showcase>();
    }

    public enum Tier
    {
        Common = 1,
        Rare,
        Legendary,
        Ultimate
    }

    public enum Mint
    {
        LimitedEdition = 1,
        CirculatingCount,
    }
}

