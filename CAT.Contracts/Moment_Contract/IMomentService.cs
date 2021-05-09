using CAT.Models;
using CAT.Models.Moment_Models;
using CAT.Models.Player_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CAT.Contracts.Moment_Contract
{
    public interface IMomentService
    {
        // Moment Service Methods
        bool CreateMoment(MomentCreate model);
        IEnumerable<MomentIndex> GetMomentIndex(Guid userId);
        MomentDetails GetMomentDetails(int id, Guid userId);
        bool EditMoment(MomentEdit model, Guid userId);
        bool DeleteMoment(int id, Guid userId);
        IEnumerable<SelectListItem> SelectPlayers(Guid userId);
        int MomentCount(Guid userId);
        decimal MomentTotalValue(Guid userId);
        decimal MomentTotalProfitLoss(Guid userId);
        decimal MomentROI(Guid userId);
    }
}
