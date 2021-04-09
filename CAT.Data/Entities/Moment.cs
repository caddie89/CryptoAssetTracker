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
        [Display(Name ="Moment ID")]
        public int MomentId { get; set; }

        [ForeignKey(nameof(Player))]
        [Display(Name = "Player ID")]
        public int? PlayerId { get; set; }
        public virtual Player Player { get; set; }

        
        [Required]
        [Display(Name = "Username")]
        public Guid OwnerId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string MomentCategory { get; set; }

        [Required]
        [Display(Name = "Date of Moment")]
        public DateTimeOffset DateOfMoment { get; set; }

        [Required]
        [Display(Name = "Set")]
        public string MomentSet { get; set; }

        [Required]
        [Display(Name = "Series")]
        public int MomentSeries { get; set; }

        [Required]
        [Display(Name = "Serial")]
        public int MomentSerialNumber { get; set; }

        [Required]
        [Display(Name = "Tier")]
        public Tier MomentTier { get; set; }

        [Required]
        [Display(Name = "Mint")]
        public Mint MomentMint { get; set; }

        [Required]
        [Display(Name = "Purchase in Pack?")]
        public bool PurchasedInPack { get; set; }

        [Display(Name = "Quantity in Pack")]
        public int AmountInPack { get; set; }

        [Required]
        [Display(Name = "Purchase Price")]
        public decimal PurchasedForPrice { get; set; }

        [Display(Name = "Actual Price")]
        public decimal ActualPurchasedForPrice
        {
            get
            {
                decimal actualPrice = PurchasedForPrice / AmountInPack;
                return actualPrice = System.Math.Round
                (actualPrice, 2);
            }
        }

        [Display(Name = "Sold Price")]
        public decimal SoldForPrice { get; set; }

        [Display(Name = "Profit/Loss")]
        public decimal ProfitLossMargin
        {
            get
            {
                decimal returnOnInvestment = ActualPurchasedForPrice - SoldForPrice;
                return returnOnInvestment = System.Math.Round(returnOnInvestment, 2);
            }
        }

        [Display(Name = "Moment")]
        public string MomentFullName
        {
            get
            {
                string momentFullName = $"{MomentCategory} - {DisplayDateOfMoment} - {MomentSet} (Series {MomentSeries})";
                return momentFullName;
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
        [Display(Name = "Limited Edition")]
        LimitedEdition = 1,
        [Display(Name = "Circulating Count")]
        CirculatingCount,
    }
}

