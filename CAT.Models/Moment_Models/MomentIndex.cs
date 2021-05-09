using CAT.Contexts.Data;
using CAT.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.Moment_Models
{
    public class MomentIndex
    {
        [Display(Name = "Moment ID")]
        public int MomentId { get; set; }

        public Guid OwnerId { get; set; }

        [Display(Name = "Player ID")]
        public int? PlayerId { get; set; }

        [Display(Name = "First Name")]
        public string PlayerFirstName { get; set; }

        [Display(Name = "Last Name")]
        public string PlayerLastName { get; set; }

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

        [Display(Name = "Individual Price")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal IndividualMomentPrice { get; set; }

        [Display(Name = "Moment Total Value")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal MomentTotalValue { get; set; }

        [Display(Name = "Sold Moment Total Value")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal SoldMomentTotalValue { get; set; }

        [Display(Name = "Original Moment Total Value")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal OriginalMomentTotalValue { get; set; }

        [Display(Name = "Moment Count")]
        public int MomentCount { get; set; }

        [Display(Name = "Sold For Amount")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal SoldForAmount { get; set; }

        public string IndividualMomentProfitLoss
        {
            get
            {
                decimal profitLoss = SoldForAmount - IndividualMomentPrice;
                profitLoss = Math.Truncate(100 * profitLoss) / 100;
                string pL = profitLoss.ToString("C", new CultureInfo("en-US"));
                return pL;
            }
        }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal TotalMomentProfitLoss
        {
            get
            {
                decimal profitLoss = SoldMomentTotalValue - OriginalMomentTotalValue;
                profitLoss = Math.Truncate(100 * profitLoss) / 100;
                return profitLoss;
            }
        }

        public string DisplayIndividualMomentPrice
        {
            get
            {
                decimal individualMomentPrice = IndividualMomentPrice;
                var iMP = individualMomentPrice.ToString("C", new CultureInfo("en-US"));
                return iMP;
            }
        }

        public decimal DisplayROI
        {
            get
            {
                if (OriginalMomentTotalValue != 0)
                {
                    var ROI = ((SoldMomentTotalValue - OriginalMomentTotalValue) / OriginalMomentTotalValue)* 100.00m;
                    return ROI;
                }
                return 0m;
            }
        }

        public string DisplayMomentTotalValue
        {
            get
            {
                var momentTotalValue = MomentTotalValue;
                var mTV = momentTotalValue.ToString("C", new CultureInfo("en-US"));
                return mTV;
            }
        }

        [Display(Name = "Moment")]
        public string MomentComplete
        {
            get
            {
                var momentFullName = $"{MomentCategory} - {DisplayDateOfMoment} - {MomentSet} (Series {MomentSeries})";
                return momentFullName;
            }
        }

        [Display(Name = "Serial")]
        public string SerialComplete
        {
            get
            {
                var serialComplete = $"{MomentSerialNumber}/{MomentCirculatingCount}";
                return serialComplete;
            }
        }

        [Display(Name = "Date")]
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

        [Display(Name = "Player")]
        public string PlayerFullName
        {
            get
            {
                var playerFullName = $"{PlayerFirstName} {PlayerLastName}";
                return playerFullName;
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

        public List<int> ShowcaseIds { get; set; }
    }
}
