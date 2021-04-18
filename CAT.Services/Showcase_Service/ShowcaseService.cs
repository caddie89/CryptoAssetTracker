using CAT.Contexts.Data;
using CAT.Models.Showcase_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Services.Showcase_Service
{
    public class ShowcaseService
    {
        private readonly Guid _userId;

        public ShowcaseService(Guid userId)
        {
            _userId = userId;
        }

        // Create Showcase
        //public bool CreateShowcase(ShowcaseCreate model)


        // Get Showcase Index
        public IEnumerable<ShowcaseIndex> GetShowcaseIndex()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Showcases
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new ShowcaseIndex
                        {
                            ShowcaseId = e.ShowcaseId,
                            ShowcaseName = e.ShowcaseName,
                            ShowcaseDescription = e.ShowcaseDescription
                        });
                return query.ToArray();
            }
        }

        // Get Showcase Details

        // Edit Showcase

        // Delete Showcase
    }
}
