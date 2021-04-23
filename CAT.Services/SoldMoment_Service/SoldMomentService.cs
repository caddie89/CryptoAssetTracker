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
                    MomentId = model.MomentId,
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
                            SoldMomentTotalValue = ctx.SoldMoments.Sum(v => v.SoldForAmount),
                            OriginalMomentTotalValue = ctx.SoldMoments.Sum(v => v.IndividualMomentPrice),
                            MomentMint = e.MomentMint,
                            SoldForAmount = e.SoldForAmount,
                        }
                    );
                return query.ToArray();
            }
        }

        // Get Sold Moment Details
        public SoldMomentDetails GetSoldMomentDetails(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .SoldMoments
                    .Single(e => e.SoldMomentId == id && e.OwnerId == _userId);
                if (entity.PlayerId == null)
                {
                    return
                    new SoldMomentDetails
                    {
                        SoldMomentId = entity.SoldMomentId,
                        PlayerFirstName = null,
                        PlayerLastName = null,
                        IndividualMomentPrice = entity.IndividualMomentPrice,
                        MomentCategory = entity.MomentCategory,
                        DateOfMoment = entity.DateOfMoment,
                        MomentSet = entity.MomentSet,
                        MomentSeries = entity.MomentSeries,
                        MomentSerialNumber = entity.MomentSerialNumber,
                        MomentCirculatingCount = entity.MomentCirculatingCount,
                        MomentTier = entity.MomentTier,
                        MomentMint = entity.MomentMint,
                        SoldForAmount = entity.SoldForAmount
                    };
                }

                return
                    new SoldMomentDetails
                    {
                        SoldMomentId = entity.SoldMomentId,
                        PlayerId = entity.PlayerId,
                        PlayerFirstName = entity.Player.PlayerFirstName,
                        PlayerLastName = entity.Player.PlayerLastName,
                        IndividualMomentPrice = entity.IndividualMomentPrice,
                        MomentCategory = entity.MomentCategory,
                        DateOfMoment = entity.DateOfMoment,
                        MomentSet = entity.MomentSet,
                        MomentSeries = entity.MomentSeries,
                        MomentSerialNumber = entity.MomentSerialNumber,
                        MomentCirculatingCount = entity.MomentCirculatingCount,
                        MomentTier = entity.MomentTier,
                        MomentMint = entity.MomentMint,
                        SoldForAmount = entity.SoldForAmount
                    };
            }
        }
    }
}
