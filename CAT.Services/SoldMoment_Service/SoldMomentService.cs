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
                            MomentTotalValue =
                            ctx
                            .Moments
                            .Where(o => o.OwnerId == _userId)
                            .Select(
                                i =>
                                i.IndividualMomentPrice)
                            .DefaultIfEmpty(0)
                            .Sum(),
                            MomentCount = ctx.Moments.Count(),
                            SoldMomentTotalValue =
                            ctx
                            .SoldMoments
                            .Where(o => o.OwnerId == _userId)
                            .Select(
                                i =>
                                i.SoldForAmount)
                            .DefaultIfEmpty(0)
                            .Sum(),
                            OriginalMomentTotalValue =
                            ctx
                            .SoldMoments
                            .Where(o => o.OwnerId == _userId)
                            .Select(
                                i =>
                                i.IndividualMomentPrice)
                            .DefaultIfEmpty(0)
                            .Sum(),
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
                        MomentId = entity.MomentId,
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
                    MomentId = entity.MomentId,
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

        // Edit Sold Moment
        public bool EditSoldMoment(SoldMomentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .SoldMoments
                    .Single(e => e.SoldMomentId == model.SoldMomentId && e.OwnerId == _userId);

                entity.MomentId = model.MomentId;
                entity.PlayerId = model.PlayerId;
                entity.MomentCategory = model.MomentCategory;
                entity.DateOfMoment = model.DateOfMoment;
                entity.MomentSet = model.MomentSet;
                entity.MomentSeries = model.MomentSeries;
                entity.MomentSerialNumber = model.MomentSerialNumber;
                entity.MomentCirculatingCount = model.MomentCirculatingCount;
                entity.MomentTier = model.MomentTier;
                entity.MomentMint = model.MomentMint;
                entity.PurchasedInPack = model.PurchasedInPack;
                entity.AmountInPack = model.AmountInPack;
                entity.PackPrice = model.PackPrice;
                entity.IndividualMomentPrice = model.IndividualMomentPrice;
                entity.SoldForAmount = model.SoldForAmount;

                return ctx.SaveChanges() >= 0;
            }
        }

        // Delete Sold Moment
        public bool DeleteSoldMoment(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .SoldMoments
                    .Single(e => e.SoldMomentId == id && e.OwnerId == _userId);

                ctx.SoldMoments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        // Moment Count
        public int MomentCount()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Moments
                    .Where(e => e.OwnerId == _userId)
                    .Count();

                var momentCount = query;

                return momentCount;
            }
        }

        // Moment Total Value
        public decimal MomentTotalValue()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Moments
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        i =>
                        i.IndividualMomentPrice)
                        .DefaultIfEmpty(0)
                        .Sum();

                var momentTotalValue = query;

                return momentTotalValue;
            }
        }

        // Total Moment ProfitLoss
        public decimal MomentTotalProfitLoss()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var soldMomentValue =
                    ctx
                    .SoldMoments
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        i =>
                        i.SoldForAmount)
                        .DefaultIfEmpty(0)
                        .Sum();

                var originalMomentValue =
                    ctx
                    .SoldMoments
                    .Where(o => o.OwnerId == _userId)
                    .Select(
                        i =>
                        i.IndividualMomentPrice)
                        .DefaultIfEmpty(0)
                        .Sum();

                decimal profitLoss = soldMomentValue - originalMomentValue;
                profitLoss = Math.Truncate(100 * profitLoss) / 100;
                return profitLoss;
            }
        }

        // Moment ROI
        public decimal MomentROI()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var soldMomentValue =
                    ctx
                    .SoldMoments
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        i =>
                        i.SoldForAmount)
                        .DefaultIfEmpty(0)
                        .Sum();

                var originalMomentValue =
                    ctx
                    .SoldMoments
                    .Where(o => o.OwnerId == _userId)
                    .Select(
                        i =>
                        i.IndividualMomentPrice)
                        .DefaultIfEmpty(0)
                        .Sum();

                if (soldMomentValue != 0)
                {
                    var ROI = (soldMomentValue - originalMomentValue) / originalMomentValue;
                    return ROI;
                }
                return 0m;
            }
        }
    }
}
