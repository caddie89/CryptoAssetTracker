using CAT.Contexts.Data;
using CAT.Data.Entities;
using CAT.Models.SoldMoment_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CAT.Services.SoldMoment_Service
{
    public class SoldMomentService
    {
        private readonly Guid _userId;

        public SoldMomentService(Guid userId)
        {
            _userId = userId;
        }

        // Create Sold Moment
        public bool CreateSoldMoment(SoldMomentCreate model)
        {
            var entity =
                new SoldMoment()
                {
                    OwnerId = _userId,
                    PlayerId = model.PlayerId,
                    MomentCategory = model.MomentCategory,
                    DateOfMoment = model.DateOfMoment,
                    MomentSet = model.MomentSet,
                    MomentSeries = model.MomentSeries,
                    MomentSerialNumber = model.MomentSerialNumber,
                    MomentCirculatingCount = model.MomentCirculatingCount,
                    MomentTier = model.MomentTier,
                    MomentMint = model.MomentMint,
                    PurchasedInPack = model.PurchasedInPack,
                    PackPrice = model.PackPrice,
                    AmountInPack = model.AmountInPack,
                    IndividualMomentPrice = model.IndividualMomentPrice,
                    SoldForAmount = model.SoldForAmount
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.SoldMoments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Get All Sold Moments
        public IEnumerable<SoldMomentIndex> GetSoldMomentIndex()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .SoldMoments
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new SoldMomentIndex
                        {
                            SoldMomentId = e.SoldMomentId,
                            PlayerId = e.Player.PlayerId,
                            PlayerFirstName = e.Player.PlayerFirstName,
                            PlayerLastName = e.Player.PlayerLastName,
                            IndividualMomentPrice = e.IndividualMomentPrice,
                            MomentCategory = e.MomentCategory,
                            DateOfMoment = e.DateOfMoment,
                            MomentSet = e.MomentSet,
                            MomentSeries = e.MomentSeries,
                            MomentSerialNumber = e.MomentSerialNumber,
                            MomentCirculatingCount = e.MomentCirculatingCount,
                            MomentTotalValue = ctx.Moments.Sum(v => v.IndividualMomentPrice),
                            MomentCount = ctx.Moments.Count(),
                            MomentMint = e.MomentMint,
                            SoldForAmount = e.SoldForAmount,
                        }
                    );
                return query.ToArray();
            }
        }

        // Populate Player Drop-Down List (Create)
        //public IEnumerable<SelectListItem> SelectPlayers()
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var query =
        //            ctx
        //            .Players
        //            .OrderBy(p => p.PlayerLastName)
        //            .Select(
        //             p =>
        //             new SelectListItem
        //             {
        //                 Text = p.PlayerFirstName + " " + p.PlayerLastName,
        //                 Value = p.PlayerId.ToString()
        //             });

        //        var playerList = query.ToList();

        //        playerList.Add(new SelectListItem { Text = "Unknown", Value = "" });
        //        playerList.Insert(0, new SelectListItem { Text = "--Select Player--", Value = "" });
        //        return playerList;
        //    }
        //}
    }
}
