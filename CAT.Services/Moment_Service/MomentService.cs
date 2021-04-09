using CAT.Contexts.Data;
using CAT.Data.Entities;
using CAT.Models.Moment_Models;
using System;
using System.Collections.Generic;
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

        // Create a Moment
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
                    MomentTier = model.MomentTier,
                    MomentMint = model.MomentMint,
                    PurchasedInPack = model.PurchasedInPack,
                    AmountInPack = model.AmountInPack,
                    PurchasedForPrice = model.PurchasedForPrice
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Moments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Populate Player drop-down list
        public IEnumerable<SelectListItem> SelectPlayers()
        {
            using (var ctx = new ApplicationDbContext())
            {                    
                var query =
                    ctx
                    .Players
                    .Select(
                     p =>
                     new SelectListItem
                     {
                         Text = p.PlayerFirstName + " " + p.PlayerLastName,
                         Value = p.PlayerId.ToString()
                     });
                
                var playerList = query.ToList();
                playerList.Insert(0, new SelectListItem { Text = "--Select Category--", Value = "" });
                playerList.Add(new SelectListItem { Text = "No Player", Value = "" });
                return playerList;
            }
        }

        // Get all Moments
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
                            PlayerFirstName = e.Player.PlayerFirstName,
                            PlayerLastName = e.Player.PlayerLastName,
                            PurchasedForPrice = e.PurchasedForPrice,
                            MomentCategory = e.MomentCategory,
                            DateOfMoment = e.DateOfMoment,
                            MomentSet = e.MomentSet,
                            MomentSeries = e.MomentSeries,
                            MomentSerialNumber = e.MomentSerialNumber,
                            AmountInPack = e.AmountInPack,
                            MomentMint = e.MomentMint
                        }
                    );
                return query.ToArray();
            }
        }

    }
}
