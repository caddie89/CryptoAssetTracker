using CAT.Models.SoldMoment_Models;
using CAT.Services.SoldMoment_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Contracts.SoldMoment_Contract
{
    public interface ISoldMomentService
    {
        bool CreateSoldMoment(SoldMomentCreate model);
        IEnumerable<SoldMomentIndex> GetSoldMomentIndex(Guid userId);
        SoldMomentDetails GetSoldMomentDetails(int id, Guid userId);
        bool EditSoldMoment(SoldMomentEdit model, Guid userId);
        bool DeleteSoldMoment(int id, Guid userId);
        int MomentCount(Guid userId);
        decimal MomentTotalValue(Guid userId);
        decimal MomentTotalProfitLoss(Guid userId);
        decimal MomentROI(Guid userId);
    }
}
