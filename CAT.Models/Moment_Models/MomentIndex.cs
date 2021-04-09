﻿using CAT.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.Moment_Models
{
    public class MomentIndex
    {
        [Display(Name = "Moment ID")]
        public int MomentId { get; set; }

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

        [Display(Name = "Tier")]
        public Tier MomentTier { get; set; }

        [Display(Name = "Mint")]
        public Mint MomentMint { get; set; }

        [Display(Name = "Purchased in Pack?")]
        public bool PurchasedInPack { get; set; }

        [Display(Name = "Quantity in Pack")]
        public int AmountInPack { get; set; }

        [Display(Name = "Purchase Price")]
        public decimal PurchasedForPrice { get; set; }

        [Display(Name = "Price")]
        public decimal ActualPurchasedForPrice
        {
            get
            {
                decimal actualPrice = PurchasedForPrice / AmountInPack;
                actualPrice = Math.Truncate(100 * actualPrice) / 100;
                return actualPrice; 
            }
        }

        [Display(Name = "Sold Price")]
        public decimal SoldForPrice { get; set; }

        [Display(Name = "Profit/Loss")]
        public decimal ProfitLossMargin
        {
            get
            {
                decimal profitLossMargin = ActualPurchasedForPrice - SoldForPrice;
                profitLossMargin = Math.Truncate(100 * profitLossMargin) / 100;
                return profitLossMargin;
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

        public virtual ICollection<Showcase> Showcases { get; set; } = new List<Showcase>();
    }
}
