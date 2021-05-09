using CAT.Contexts.Data;
using CAT.Contracts.Moment_Contract;
using CAT.Contracts.Player_Contract;
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
    public class PlayerService : IPlayerService
    {
        // Create Player
        public bool CreatePlayer(PlayerCreate model)
        {
            var entity =
                new Player()
                {
                    OwnerId = model.OwnerId,
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
        public IEnumerable<PlayerIndex> GetPlayerIndex(string search, int? page, Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                if (search == null)
                {
                    var searchPlayers =
                    ctx
                    .Players
                    .Where(e => e.OwnerId == userId)
                    .OrderBy(p => p.PlayerLastName)
                    .Select(
                        e =>
                        new PlayerIndex
                        {
                            OwnerId = e.OwnerId,
                            PlayerId = e.PlayerId,
                            PlayerFirstName = e.PlayerFirstName,
                            PlayerLastName = e.PlayerLastName,
                            PositionOfPlayer = e.PositionOfPlayer,
                            PlayerTeam = e.PlayerTeam
                        });
                    return searchPlayers.ToArray().ToPagedList(page ?? 1, 9);
                }
                else
                {
                    var searchPlayers =
                    ctx
                    .Players
                    .Where(e => e.OwnerId == userId && (e.PlayerLastName.StartsWith(search) || e.PlayerFirstName.StartsWith(search)))
                    .OrderBy(p => p.PlayerLastName)
                    .Select(
                        e =>
                        new PlayerIndex
                        {
                            OwnerId = e.OwnerId,
                            PlayerId = e.PlayerId,
                            PlayerFirstName = e.PlayerFirstName,
                            PlayerLastName = e.PlayerLastName,
                            PositionOfPlayer = e.PositionOfPlayer,
                            PlayerTeam = e.PlayerTeam
                        });
                    return searchPlayers.ToArray().ToPagedList(page ?? 1, 9);
                }
            }
        }

        // Get Player Details
        public PlayerDetails GetPlayerDetails(int id, Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Players
                    .Single(e => e.PlayerId == id && e.OwnerId == userId);
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
        public bool EditPlayer(PlayerEdit model, Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Players
                    .Single(e => e.PlayerId == model.PlayerId && e.OwnerId == userId);

                entity.PlayerFirstName = model.PlayerFirstName;
                entity.PlayerLastName = model.PlayerLastName;
                entity.PositionOfPlayer = model.PositionOfPlayer;
                entity.PlayerTeam = model.PlayerTeam;

                return ctx.SaveChanges() >= 0;
            }
        }

        // Delete Player
        public bool DeletePlayer(int id, Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.
                    Players.Single(e => e.PlayerId == id && e.OwnerId == userId);

                ctx.Players.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
