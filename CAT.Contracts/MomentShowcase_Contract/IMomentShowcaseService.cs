using CAT.Models.MomentShowcase_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CAT.Contracts.MomentShowcase_Contract
{
    public interface IMomentShowcaseService
    {
        bool CreateMomentShowcase(MomentShowcaseCreate model);
        MomentShowcaseDetails GetMomentShowcaseDetails(int momentId, int showcaseId, Guid userId);
        bool DeleteMomentShowcase(int momentId, int showcaseId, Guid userId);
        IEnumerable<SelectListItem> SelectMoment(Guid userId);
        IEnumerable<SelectListItem> SelectShowcase(Guid userId);
    }
}
