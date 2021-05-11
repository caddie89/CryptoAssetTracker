using CAT.Contracts.PlayerAPI_Contract;
using CAT.Models.PlayerAPI_Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CAT.WebMVC.Controllers.PlayerAPI
{
    public class PlayerAPIController : Controller
    {
        private Guid _userId;
        private readonly IPlayerAPIService _playerAPIService;
        private readonly string _baseurl = "https://www.balldontlie.io/api/v1/";

        public PlayerAPIController(IPlayerAPIService playerAPIService)
        {
            _playerAPIService = playerAPIService;
        }

        // Ogle at its brilliance, Simon!
        public async Task<ActionResult> Details(int id)
        {
            RootAPIModel playerInfo = new RootAPIModel();
            RootAPIModel list = new RootAPIModel();
            list.data = new List<PlayerAPIModel>();

            _userId = Guid.Parse(User.Identity.GetUserId());

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var storedPlayers = _playerAPIService.GetPlayerDetails(id, _userId);

                HttpResponseMessage Res = await client.GetAsync($"players?search={storedPlayers.PlayerLastName}&per_page=100");

                if (Res.IsSuccessStatusCode)
                {
                    var PlayerResponse = Res.Content.ReadAsStringAsync().Result;

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    playerInfo = JsonConvert.DeserializeObject<RootAPIModel>(PlayerResponse, settings);
                    list.data.AddRange(playerInfo.data);
                }

                foreach (var player in list.data)
                {
                    if (player.first_name == storedPlayers.PlayerFirstName && player.last_name == storedPlayers.PlayerLastName)
                    {
                        storedPlayers.id = player.team.id;
                        storedPlayers.abbreviation = player.team.abbreviation;
                        storedPlayers.division = player.team.division;
                        storedPlayers.conference = player.team.conference;
                        storedPlayers.height_feet = player.height_feet;
                        storedPlayers.height_inches = player.height_inches;
                        storedPlayers.weight_pounds = player.weight_pounds;

                        return View(storedPlayers);
                    }
                }
                return View(storedPlayers);
            }
        }

        // Don't worry about this shame, Simon! - THIS METHOD IS NOT USED ANYWHERE
        public async Task<List<PlayerAPIModel>> PlayerInfo()
        {
            RootAPIModel playerInfo = new RootAPIModel();
            RootAPIModel list = new RootAPIModel();

            list.data = new List<PlayerAPIModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var page = 1;

                while (page <= 34)
                {
                    HttpResponseMessage Res = await client.GetAsync($"players?per_page=100&page={page}");

                    if (Res.IsSuccessStatusCode)
                    {
                        var PlayerResponse = await Res.Content.ReadAsStringAsync();
                        playerInfo = JsonConvert.DeserializeObject<RootAPIModel>(PlayerResponse);
                        list.data.AddRange(playerInfo.data);
                    }

                    if (playerInfo.meta.current_page <= 34)
                    {
                        page++;
                    }
                }
                return list.data;
            }
        }
    }
}


