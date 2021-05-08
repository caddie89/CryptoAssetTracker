using CAT.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CAT.Models.Moment_Models
{
    public class MomentEdit
    {
        [Display(Name = "Moment ID")]
        public int MomentId { get; set; }

        [Display(Name = "Player ID")]
        public int? PlayerId { get; set; }

        [Display(Name = "Category")]
        public string MomentCategory { get; set; }

        [Display(Name = "Date of Moment")]
        public DateTimeOffset DateOfMoment { get; set; }

        [Display(Name = "Set")]
        public string MomentSet { get; set; }

        [Display(Name = "Series")]
        public int MomentSeries { get; set; }

        [Display(Name = "Serial")]
        public int MomentSerialNumber { get; set; }

        [Display(Name = "Circulating Count")]
        public int MomentCirculatingCount { get; set; }

        [Display(Name = "Tier")]
        public Tier MomentTier { get; set; }

        [Display(Name = "Mint")]
        public Mint MomentMint { get; set; }

        [Display(Name = "Purchased in Pack?")]
        public bool PurchasedInPack { get; set; }

        [Display(Name = "Pack Price")]
        public decimal? PackPrice { get; set; }

        [Display(Name = "Amount in Pack")]
        public decimal? AmountInPack { get; set; }

        [Display(Name = "Individual Price")]
        public decimal IndividualMomentPrice { get; set; }
    }
}
