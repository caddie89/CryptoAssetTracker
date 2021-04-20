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
        public decimal PurchasedForPrice { get; set; }

        //[Required]
        public bool PurchasedInPack { get; set; } 

        //[Required]
        public decimal AmountInPack { get; set; }

        public decimal SoldForPrice { get; set; }

        public decimal ActualPurchasedForPrice
        {
            get
            {
                if (PurchasedInPack is false)
                {
                    return PurchasedForPrice;
                }

                decimal actualPrice = PurchasedForPrice / AmountInPack;
                actualPrice = Math.Truncate(100 * actualPrice) / 100;
                return actualPrice;
            }
        }

        public string MomentComplete
        {
            get
            {
                var momentFullName = $"{MomentCategory} - {DisplayDateOfMoment} - {MomentSet} (Series {MomentSeries})";
                return momentFullName;
            }
        }

        public string SerialComplete
        {
            get
            {
                var serialComplete = $"{MomentSerialNumber}/{MomentCirculatingCount}";
                return serialComplete;
            }
        }

        public string DisplayDateOfMoment
        {
            get
            {
                var date = DateOfMoment;
                string fmt = "MMMM dd yyyy";
                var displayDate = date.Date.ToString(fmt);
                return displayDate;
            }
        }

        public string Mint
        {
            get
            {
                int mintValue = (int)MomentMint;
                string mintType = Enum.GetName(typeof(Mint), mintValue);

                switch (mintValue)
                {
                    case 1:
                        mintType = "LE";
                        break;
                    case 2:
                        mintType = "CC";
                        break;
                }
                return mintType;
            }
        }

        public virtual ICollection<MomentShowcase> Showcases { get; set; } = new List<MomentShowcase>();
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
        [Display(Name = "Limited Edition")]
        LimitedEdition = 1,
        [Display(Name = "Circulating Count")]
        CirculatingCount,
    }
}

