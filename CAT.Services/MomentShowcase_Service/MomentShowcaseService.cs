using CAT.Contexts.Data;
using CAT.Data.Entities;
using CAT.Models.MomentShowcase_Models;
using System;
using System.Collections.Generic;
using System.Data;
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
                    ShowcaseId = model.ShowcaseId,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.MomentsShowcases.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //foreach (int bookid in model.Moments)
        //    {
        //        var entity - new AuthorBook()
        //{ AuthorId = model.AuthorId,
        //BookId = bookId
        //        }
        //    }

        // Create MomentShowcase
        //public bool CreateMomentShowcase(MomentShowcaseCreate model)
        //{
        //    var entity =
        //        new MomentShowcase()
        //        {
        //            OwnerId = _userId,
        //            ShowcaseId = model.ShowcaseId,
        //            MomentId = model.
        //        };

        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        ctx.MomentsShowcases.Add(entity);
        //        ctx.SaveChanges();

        //        foreach (int momentId in model.SelectedMomentIds)
        //        {
        //            var moment = ctx.Moments.Find(momentId);
        //            if (moment != null)
        //            {
        //                entity.Moments.Add(moment);
        //            }
        //        }
        //        return ctx.SaveChanges() == 1;
        //    }
        //}

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
        public IEnumerable<SelectListItem> SelectMoment()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Moments
                    .OrderBy(p => p.IndividualMomentPrice)
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

                momentList.Insert(0, new SelectListItem { Text = "--Select Moment--", Value = "" });
                return momentList;
            }
        }

        // Populate Showcase Drop-Down List (Create)
        public IEnumerable<SelectListItem> SelectShowcase()
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

                showcaseList.Insert(0, new SelectListItem { Text = "--Select Showcase--", Value = "" });
                return showcaseList;
            }
        }

        // Populate Moment Drop-Down List
        //public MultiSelectList SelectMoments()
        //{
        //    var viewModel = new MomentShowcaseCreate();

        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var moments =
        //            ctx
        //            .Moments
        //            .OrderBy(p => p.Player.PlayerLastName)
        //            .Select(
        //            moment =>
        //            new
        //            {
        //                MomentId = moment.MomentId,
        //                CompleteMoment =
        //                moment.Player.PlayerFirstName + " " +
        //                moment.Player.PlayerLastName + "-" +
        //                moment.MomentCategory + "-" +
        //                moment.MomentSet + "-Series " +
        //                moment.MomentSeries
        //            }).ToList();

        //        viewModel.MomentList = new MultiSelectList(moments, "MomentId", "CompleteMoment");

        //        return viewModel.MomentList;
        //    }
        //}

        //public List<MomentIndex> GetMoments(){
    }
}
