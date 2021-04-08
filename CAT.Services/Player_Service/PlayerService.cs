using CAT.Contexts.Data;
using CAT.Data.Entities;
using CAT.Models;
using CAT.Models.Player_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CAT.Services.Player_Service
{
    public class PlayerService
    {
        private readonly Guid _userId;

        public PlayerService(Guid userId)
        {
            _userId = userId;
        }

        // Create a Player
        public bool CreatePlayer(PlayerCreate model)
        {
            var entity =
                new Player()
                {
                    OwnerId = _userId,
                    PlayerFirstName = model.PlayerFirstName,
                    PlayerLastName = model.PlayerLastName,
                    PositionOfPlayer = model.PositionOfPlayer,
                    //TeamId = model.TeamId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Players.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Get all Players
        public IEnumerable<PlayerIndex> GetPlayerIndex()
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
                return query.ToArray();
            }
        }

        // Get Player Details
        public PlayerDetail GetPlayerDetail(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Players
                    .Single(e => e.PlayerId == id && e.OwnerId == _userId);
                return
                    new PlayerDetail
                    {
                        PlayerId = entity.PlayerId,
                        PlayerFirstName = entity.PlayerFirstName,
                        PlayerLastName = entity.PlayerLastName,
                        PositionOfPlayer = entity.PositionOfPlayer,
                        PlayerTeam = entity.PlayerTeam
                    };
            }
        }

        // Edit a Player

    }
}
