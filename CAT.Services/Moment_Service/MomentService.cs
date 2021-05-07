using CAT.Contexts.Data;
using CAT.Data.Entities;
using CAT.Models.Moment_Models;
using CAT.Models.Showcase_Models;
using CAT.Models.SoldMoment_Models;
using LinqToDB.SqlQuery;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CAT.Services.Moment_Service
{
    public class MomentService
    {
        private readonly Guid _userId;

        public MomentService(Guid userId)
        {
            _userId = userId;
        }

        // Create Moment
        public bool CreateMoment(MomentCreate model)
        {
            var entity =
                new Moment()
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
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Moments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Get All Moments
        public IEnumerable<MomentIndex> GetMomentIndex()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Moments
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new MomentIndex
                        {
                            MomentId = e.MomentId,
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
                                s =>
                                s.SoldForAmount)
                            .DefaultIfEmpty(0)
                            .Sum(),
                            OriginalMomentTotalValue =
                            ctx
                            .SoldMoments
                            .Where(o => o.OwnerId == _userId)
                            .Select(
                                s =>
                                s.IndividualMomentPrice)
                            .DefaultIfEmpty(0)
                            .Sum(),
                            MomentMint = e.MomentMint,
                            ShowcaseIds = e.Showcases
                            .Where(o => o.OwnerId == _userId)
                            .Select(
                                s =>
                                s.Showcase.ShowcaseId).ToList()
                        }
                    );
                return query.ToArray();
            }
        }

        // Get Moment Details
        public MomentDetails GetMomentDetails(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Moments
                    .Single(e => e.MomentId == id && e.OwnerId == _userId);

                if (entity.PlayerId == null)
                {
                    return
                    new MomentDetails
                    {
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
                        Showcases = entity.Showcases
                        .Select(
                            e =>
                            new ShowcaseIndex()
                            {
                                ShowcaseId = e.Showcase.ShowcaseId,
                                ShowcaseName = e.Showcase.ShowcaseName,
                                ShowcaseDescription = e.Showcase.ShowcaseDescription
                            }).ToList()
                    };
                }

                return
                new MomentDetails
                {
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
                    Showcases = entity.Showcases
                        .Select(
                            e =>
                            new ShowcaseIndex()
                            {
                                ShowcaseId = e.Showcase.ShowcaseId,
                                ShowcaseName = e.Showcase.ShowcaseName,
                                ShowcaseDescription = e.Showcase.ShowcaseDescription
                            }).ToList()
                };
            }
        }

        // Edit Moment
        public bool EditMoment(MomentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Moments
                    .Single(e => e.MomentId == model.MomentId && e.OwnerId == _userId);

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

                return ctx.SaveChanges() >= 0;
            }
        }

        // Delete Moment
        public bool DeleteMoment(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Moments
                    .Single(e => e.MomentId == id && e.OwnerId == _userId);

                ctx.Moments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        // Populate Player Drop-Down List
        public IEnumerable<SelectListItem> SelectPlayers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Players
                    .OrderBy(p => p.PlayerLastName)
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                     p =>
                     new SelectListItem
                     {
                         Text = p.PlayerFirstName + " " + p.PlayerLastName,
                         Value = p.PlayerId.ToString()
                     });

                var playerList = query.ToList();

                playerList.Add(new SelectListItem { Text = "Unknown", Value = "" });
                playerList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
                return playerList;
            }
        }

        // Populate Player Drop-Down List (Edit)
        public List<SelectListItem> PlayersList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Players
                    .OrderBy(p => p.PlayerLastName)
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                     p =>
                     new SelectListItem
                     {
                         Text = p.PlayerFirstName + " " + p.PlayerLastName,
                         Value = p.PlayerId.ToString()
                     });

                var playerList = query.ToList();

                playerList.Add(new SelectListItem { Text = "Unknown", Value = "" });
                playerList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
                return playerList;
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

                if (originalMomentValue != 0)
                {
                    decimal ROI = ((soldMomentValue - originalMomentValue) / originalMomentValue)*100.00m;
                    ROI = Math.Round(ROI, 2);
                    return ROI;
                }
                return 0m;
            }
        }
    }
}

