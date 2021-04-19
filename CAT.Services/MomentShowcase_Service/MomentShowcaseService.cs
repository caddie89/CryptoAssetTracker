using CAT.Contexts.Data;
using CAT.Data.Entities;
using CAT.Models.MomentShowcase_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Services.MomentShowcase_Service
{
    public class MomentShowcaseService
    {
        private readonly Guid _userId;

        public MomentShowcaseService(Guid userId)
        {
            _userId = userId;
        }

        // Create MomentShowcase
        public bool CreateMomentShowcase(MomentShowcaseCreate model)
        {
            var entity =
                new MomentShowcase()
                {
                    MomentId = model.MomentId,
                    ShowcaseId = model.ShowcaseId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.MomentsShowcases.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
