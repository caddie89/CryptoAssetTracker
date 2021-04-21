using CAT.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.Moment_Models
{
    public class MomentCreate
    {
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
        [Display(Name = "Circulating Count")]
        public int MomentCirculatingCount { get; set; }

        [Required]
        [Display(Name = "Tier")]
        public Tier MomentTier { get; set; }

        [Required]
        [Display(Name = "Mint")]
        public Mint MomentMint { get; set; }

        [Required]
        [Display(Name = "Purchased in Pack?")]
        public bool PurchasedInPack { get; set; }

        [Display(Name = "Pack Price")]
        public decimal? PackPrice { get; set; }

        [Display(Name = "Amount in Pack")]
        public decimal? AmountInPack { get; set; }

        [Required]
        [Display(Name = "Individual Price")]
        public decimal IndividualMomentPrice { get; set; }
    }
}
