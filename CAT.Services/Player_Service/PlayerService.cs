using CAT.Contexts.Data;
using CAT.Models;
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
                            PositionOfPlayer = (PlayerPosition)e.PositionOfPlayer,
                            PlayerTeam = e.PlayerTeam
                        });
                return query.ToArray();
            }
        }
    }
}
