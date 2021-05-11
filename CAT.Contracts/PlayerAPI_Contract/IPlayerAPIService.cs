using CAT.Models.Player_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Contracts.PlayerAPI_Contract
{
    public interface IPlayerAPIService
    {
        PlayerDetails GetPlayerDetails(int id, Guid userId);
    }
}
