using CAT.Contexts.Data;
using CAT.Data.Entities;
using CAT.Models.MomentShowcase_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CAT.Services.MomentShowcase_Service
{
    public class MomentShowcaseService
    {
        private readonly Guid _userId;

        public MomentShowcaseService(Guid userId)
        {
            _userId = userId;
        }

        // Create MomentShowcase
        public bool CreateMomentShowcase(MomentShowcaseCreate model)
        {
            var entity =
                new MomentShowcase()
                {
                    OwnerId = _userId,
                    MomentId = model.MomentId,
                    ShowcaseId = model.ShowcaseId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.MomentsShowcases.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Get Showcase Details
        public MomentShowcaseDetails GetMomentShowcaseDetails(int momentId, int showcaseId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .MomentsShowcases
                    .Single(e => e.MomentId == momentId && e.ShowcaseId == showcaseId && e.OwnerId == _userId);

                return
                new MomentShowcaseDetails
                {
                    MomentId = entity.MomentId,
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
        public bool DeleteMomentShowcase(int momentId, int showcaseId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .MomentsShowcases
                    .SingleOrDefault(e => e.MomentId == momentId && e.ShowcaseId == showcaseId && e.OwnerId == _userId);

                if (entity != null)
                {
                    ctx.MomentsShowcases.Remove(entity);
                    return ctx.SaveChanges() == 1;
                }
                return false;
            }
        }

        // Populate Moment Drop-Down List (Create)
        public IEnumerable<SelectListItem> SelectMoments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Moments
                    .OrderByDescending(p => p.Player.PlayerLastName)
                    .Select(
                     p =>
                     new SelectListItem
                     {
                         Text =
                         p.Player.PlayerFirstName + " " +
                         p.Player.PlayerLastName + " - " +
                         p.MomentCategory + " - " +
                         p.MomentSet + " - " +
                         p.MomentSeries.ToString(),
                         Value = p.MomentId.ToString()
                     });

                var momentList = query.ToList();

                momentList.Add(new SelectListItem { Text = "Unknown", Value = "" });
                momentList.Insert(0, new SelectListItem { Text = "--Select Moment--", Value = "" });
                return momentList;
            }
        }

        // Populate Showcase Drop-Down List (Create)
        public IEnumerable<SelectListItem> SelectShowcases()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Showcases
                    .OrderBy(p => p.ShowcaseName)
                    .Select(
                     p =>
                     new SelectListItem
                     {
                         Text = p.ShowcaseName.ToString(),
                         Value = p.ShowcaseId.ToString()
                     });

                var showcaseList = query.ToList();

                showcaseList.Add(new SelectListItem { Text = "Unknown", Value = "" });
                showcaseList.Insert(0, new SelectListItem { Text = "--Select Showcase--", Value = "" });
                return showcaseList;
            }
        }
    }
}
