using CAT.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.Moment_Models
{
    public class MomentCreate
    {
        [Required]
        [Display(Name = "Moment ID")]
        public int MomentId { get; set; }

        [Required]
        [Display(Name = "Player ID")]
        public int? PlayerId { get; set; }

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
    }
}
