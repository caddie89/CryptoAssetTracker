﻿using CAT.Contexts.Data;
using CAT.Contracts.PlayerAPI_Contract;
using CAT.Models.Moment_Models;
using CAT.Models.Player_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Services.PlayerAPI_Service
{
    public class PlayerAPIService : IPlayerAPIService
    {
        public PlayerDetails GetPlayerDetails(int id, Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Players
                    .Single(e => e.PlayerId == id && e.OwnerId == userId);

                return
                new PlayerDetails
                {
                    PlayerId = entity.PlayerId,
                    PlayerFirstName = entity.PlayerFirstName,
                    PlayerLastName = entity.PlayerLastName,
                    PositionOfPlayer = entity.PositionOfPlayer,
                    PlayerTeam = entity.PlayerTeam,
                    id = null,
                    abbreviation = "Unknown",
                    division = "Unknown",
                    conference = "Unknown",
                    Moments = entity.Moments
                    .Select(
                    m =>
                    new MomentIndex()
                    {
                        MomentId = m.MomentId,
                        PlayerId = m.PlayerId,
                        MomentCategory = m.MomentCategory,
                        DateOfMoment = m.DateOfMoment,
                        MomentSet = m.MomentSet,
                        MomentSeries = m.MomentSeries,
                    }).ToList()
                };
            }
        }
    }
}
