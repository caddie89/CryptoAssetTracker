using CAT.Contexts.Data;
using CAT.Data.Entities;
using CAT.Models;
using CAT.Models.Moment_Models;
using CAT.Models.Player_Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Services.Player_Service
{
    public class PlayerService
    {
        private readonly Guid _userId;

        public PlayerService(Guid userId)
        {
            _userId = userId;
        }

        // Create Player
        public bool CreatePlayer(PlayerCreate model)
        {
            var entity =
                new Player()
                {
                    OwnerId = _userId,
                    PlayerFirstName = model.PlayerFirstName,
                    PlayerLastName = model.PlayerLastName,
                    PositionOfPlayer = model.PositionOfPlayer,
                    PlayerTeam = model.PlayerTeam
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Players.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Get Player Index
        public IEnumerable<PlayerIndex> GetPlayerIndex(int? page)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Players
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new PlayerIndex
                        {
                            PlayerId = e.PlayerId,
                            PlayerFirstName = e.PlayerFirstName,
                            PlayerLastName = e.PlayerLastName,
                            PositionOfPlayer = e.PositionOfPlayer,
                            PlayerTeam = e.PlayerTeam
                        });

                    return query.ToArray().ToPagedList(page ?? 1, 6);
            }
        }

        // Get Player Details
        public PlayerDetails GetPlayerDetails(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Players
                    .Single(e => e.PlayerId == id && e.OwnerId == _userId);
                return
                    new PlayerDetails
                    {
                        PlayerId = entity.PlayerId,
                        PlayerFirstName = entity.PlayerFirstName,
                        PlayerLastName = entity.PlayerLastName,
                        PositionOfPlayer = entity.PositionOfPlayer,
                        PlayerTeam = entity.PlayerTeam,
                        Moments = entity.Moments
                        .Select(
                        m =>
                        new MomentIndex()
                        {
                            MomentId = m.MomentId,
                            PlayerId = m.PlayerId,
                            MomentCategory = m.MomentCategory,
                            DateOfMoment = m.DateOfMoment,
                            MomentSet = m.MomentSet,
                            MomentSeries = m.MomentSeries,
                        }).ToList()
                    };
            }
        }

        // Edit Player
        public bool EditPlayer(PlayerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Players
                    .Single(e => e.PlayerId == model.PlayerId && e.OwnerId == _userId);

                entity.PlayerFirstName = model.PlayerFirstName;
                entity.PlayerLastName = model.PlayerLastName;
                entity.PositionOfPlayer = model.PositionOfPlayer;
                entity.PlayerTeam = model.PlayerTeam;

                return ctx.SaveChanges() == 1;
            }
        }

        // Delete Player
        public bool DeletePlayer(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.
                    Players.Single(e => e.PlayerId == id && e.OwnerId == _userId);

                ctx.Players.Remove(entity);

                return ctx.SaveChanges() > 0;
            }
        }

    }
}
