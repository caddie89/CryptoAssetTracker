﻿using CAT.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.SoldMoment_Models
{
    public class SoldMomentIndex
    {
        [Display(Name = "Moment ID")]
        public int MomentId { get; set; }

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

        [Display(Name = "Purchased in Pack?")]
        public bool PurchasedInPack { get; set; }

        [Display(Name = "Quantity in Pack")]
        public decimal AmountInPack { get; set; }

        [Display(Name = "Purchase Price")]
        public decimal PurchasedForPrice { get; set; }

        [Display(Name = "Sold For Amount")]
        public decimal SoldForAmount { get; set; }

        [Display(Name = "Profit/Loss")]
        public decimal ProfitLoss
        {
            get
            {
                decimal profitLoss = SoldForAmount - ActualPurchasedForPrice;
                profitLoss = Math.Truncate(100 * profitLoss) / 100;
                return profitLoss;
            }
        }

        [Display(Name = "Price")]
        public decimal ActualPurchasedForPrice
        {
            get
            {
                if (AmountInPack < 1)
                {
                    return PurchasedForPrice;
                }
                else
                {
                    decimal actualPrice = PurchasedForPrice / AmountInPack;
                    actualPrice = Math.Truncate(100 * actualPrice) / 100;
                    return actualPrice;
                }
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
    }
}