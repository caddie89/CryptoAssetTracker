using CAT.Contexts.Data;
using CAT.Data.Entities;
using CAT.Models;
using CAT.Models.Player_Models;
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

        // CREATE a player
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

        // GET all Players
        public IEnumerable<PlayerIndex> GetAllPlayers()
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
                            PlayerFirstName = e.PlayerFirstName,
                            PlayerLastName = e.PlayerLastName,
                            PositionOfPlayer = e.PositionOfPlayer,
                            PlayerTeam = e.PlayerTeam
                        });
                return query.ToArray();
            }
        }
    }
}
