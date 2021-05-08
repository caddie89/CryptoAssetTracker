using CAT.Contexts.Data;
using CAT.Data.Entities;
using CAT.Models.Moment_Models;
using CAT.Models.Showcase_Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CAT.Services.Showcase_Service
{
    public class ShowcaseService
    {
        private readonly Guid _userId;

        public ShowcaseService(Guid userId)
        {
            _userId = userId;
        }

        // Create Showcase
        public bool CreateShowcase(ShowcaseCreate model)
        {
            var entity =
                new Showcase()
                {
                    OwnerId = _userId,
                    ShowcaseName = model.ShowcaseName,
                    ShowcaseDescription = model.ShowcaseDescription,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Showcases.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Get Showcase Index
        public IEnumerable<ShowcaseIndex> GetShowcaseIndex()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Showcases
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new ShowcaseIndex
                        {
                            ShowcaseId = e.ShowcaseId,
                            ShowcaseName = e.ShowcaseName,
                            ShowcaseDescription = e.ShowcaseDescription,
                            MomentIds = e.Moments
                            .Where(o => o.OwnerId == _userId)
                            .Select(
                                s =>
                                s.Moment.MomentId).ToList(),
                            MomentIdsCount = e.Moments.Count()
                        });
                return query.ToArray();
            }
        }

        // Get Showcase Details
        public ShowcaseDetails GetShowcaseDetails(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Showcases
                    .Single(e => e.ShowcaseId == id && e.OwnerId == _userId);

                return
                new ShowcaseDetails
                {
                    ShowcaseId = entity.ShowcaseId,
                    ShowcaseName = entity.ShowcaseName,
                    ShowcaseDescription = entity.ShowcaseDescription,
                    Moments = entity.Moments
                    .Select(
                        m =>
                        new MomentIndex()
                        {
                            MomentId = m.MomentId,
                            PlayerId = m.Moment.PlayerId,
                            PlayerFirstName = m.Moment.Player.PlayerFirstName,
                            PlayerLastName = m.Moment.Player.PlayerLastName,
                            IndividualMomentPrice = m.Moment.IndividualMomentPrice,
                            MomentCategory = m.Moment.MomentCategory,
                            DateOfMoment = m.Moment.DateOfMoment,
                            MomentSet = m.Moment.MomentSet,
                            MomentSeries = m.Moment.MomentSeries,
                            MomentSerialNumber = m.Moment.MomentSerialNumber,
                            MomentCirculatingCount = m.Moment.MomentCirculatingCount,
                            MomentTier = m.Moment.MomentTier,
                            MomentMint = m.Moment.MomentMint,
                        }).ToList()
                };
            }
        }

        // Edit Showcase
        public bool EditShowcase(ShowcaseEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Showcases
                    .Single(e => e.ShowcaseId == model.ShowcaseId && e.OwnerId == _userId);

                entity.ShowcaseName = model.ShowcaseName;
                entity.ShowcaseDescription = model.ShowcaseDescription;

                return ctx.SaveChanges() >= 0;
            }
        }

        // Delete Showcase
        public bool DeleteShowcase(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Showcases
                    .Single(e => e.ShowcaseId == id && e.OwnerId == _userId);

                ctx.Showcases.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
