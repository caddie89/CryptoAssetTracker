using CAT.Models;
using CAT.Models.Player_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Contracts.Player_Contract
{
    public interface IPlayerService
    {
        bool CreatePlayer(PlayerCreate model);
        IEnumerable<PlayerIndex> GetPlayerIndex(string search, int? page, Guid userId);
        PlayerDetails GetPlayerDetails(int id, Guid userId);
        bool EditPlayer(PlayerEdit model, Guid userId);
        bool DeletePlayer(int id, Guid userId);
    }
}
