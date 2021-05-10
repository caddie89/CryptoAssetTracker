using CAT.Contexts.Data;
using CAT.Contracts.MomentShowcase_Contract;
using CAT.Data.Entities;
using CAT.Models.MomentShowcase_Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xceed.Wpf.Toolkit;

namespace CAT.Services.MomentShowcase_Service
{
    public class MomentShowcaseService : IMomentShowcaseService
    {
        // Create MomentShowcase
        public bool CreateMomentShowcase(MomentShowcaseCreate model)
        {
            var entity =
                new MomentShowcase()
                {
                    OwnerId = model.OwnerId,
                    MomentId = model.MomentId,
                    ShowcaseId = model.ShowcaseId,
                };

            using (var ctx = new ApplicationDbContext())
            {
                // Error Handling
                var exists = ctx.MomentsShowcases.Where(p => p.MomentId == entity.MomentId && p.ShowcaseId == entity.ShowcaseId).Count();
                if (exists > 0)
                {
                    return false;
                }
                ctx.MomentsShowcases.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Get Showcase Details
        public MomentShowcaseDetails GetMomentShowcaseDetails(int momentId, int showcaseId, Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .MomentsShowcases
                    .Single(e => e.MomentId == momentId && e.ShowcaseId == showcaseId && e.OwnerId == userId);

                if (entity.Moment.PlayerId == null)
                {
                    return
                    new MomentShowcaseDetails
                    {
                        MomentId = entity.MomentId,
                        PlayerFirstName = null,
                        PlayerLastName = null,
                        ShowcaseId = entity.ShowcaseId,
                        ShowcaseName = entity.Showcase.ShowcaseName,
                        MomentCategory = entity.Moment.MomentCategory,
                        DateOfMoment = entity.Moment.DateOfMoment,
                        MomentSet = entity.Moment.MomentSet,
                        MomentSeries = entity.Moment.MomentSeries,
                    };
                }
                return
                new MomentShowcaseDetails
                {
                    MomentId = entity.MomentId,
                    PlayerFirstName = entity.Moment.Player.PlayerFirstName,
                    PlayerLastName = entity.Moment.Player.PlayerLastName,
                    ShowcaseId = entity.ShowcaseId,
                    ShowcaseName = entity.Showcase.ShowcaseName,
                    MomentCategory = entity.Moment.MomentCategory,
                    DateOfMoment = entity.Moment.DateOfMoment,
                    MomentSet = entity.Moment.MomentSet,
                    MomentSeries = entity.Moment.MomentSeries,
                };
            }
        }

        // Delete MomentShowcase
        public bool DeleteMomentShowcase(int momentId, int showcaseId, Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .MomentsShowcases
                    .SingleOrDefault(e => e.MomentId == momentId && e.ShowcaseId == showcaseId && e.OwnerId == userId);

                if (entity != null)
                {
                    ctx.MomentsShowcases.Remove(entity);
                    return ctx.SaveChanges() == 1;
                }
                return false;
            }
        }

        // Populate Moment Drop-Down List (Create)
        public IEnumerable<SelectListItem> SelectMoment(Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Moments
                    .OrderBy(p => p.IndividualMomentPrice)
                    .Where(e => e.OwnerId == userId)
                    .Select(
                     p =>
                     new SelectListItem
                     {
                         Text =
                         p.IndividualMomentPrice + " - " +
                         p.Player.PlayerFirstName + " " +
                         p.Player.PlayerLastName + " - " +
                         p.MomentCategory + " - " +
                         p.MomentSet + " - Series " +
                         p.MomentSeries.ToString(),
                         Value = p.MomentId.ToString()
                     });

                var momentList = query.ToList();

                momentList.Insert(0, new SelectListItem { Text = "Select Asset", Value = "" });
                return momentList;
            }
        }

        // Populate Showcase Drop-Down List (Create)
        public IEnumerable<SelectListItem> SelectShowcase(Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Showcases
                    .OrderBy(p => p.ShowcaseName)
                    .Where(e => e.OwnerId == userId)
                    .Select(
                     p =>
                     new SelectListItem
                     {
                         Text = p.ShowcaseName.ToString(),
                         Value = p.ShowcaseId.ToString()
                     });

                var showcaseList = query.ToList();

                showcaseList.Insert(0, new SelectListItem { Text = "Select Collection", Value = "" });

                return showcaseList;
            }
        }
    }
}
