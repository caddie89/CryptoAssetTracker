using CAT.Models.Showcase_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Contracts.Showcase_Contract
{
    public interface IShowcaseService
    {
        bool CreateShowcase(ShowcaseCreate model);
        IEnumerable<ShowcaseIndex> GetShowcaseIndex(Guid userId);
        ShowcaseDetails GetShowcaseDetails(int id, Guid userId);
        bool EditShowcase(ShowcaseEdit model, Guid userId);
        bool DeleteShowcase(int id, Guid userId);
    }
}
