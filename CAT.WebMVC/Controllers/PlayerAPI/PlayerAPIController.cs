using CAT.Models.PlayerAPI_Models;
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
        //private readonly HttpClient _client;
        //private readonly string _url = "https://www.balldontlie.io/api/v1/team/1";
        //private readonly string _urlPlayers = "https://www.balldontlie.io/api/v1/players";

        //public PlayerAPIController()
        //{
        //    _client = new HttpClient();
        //    _client.BaseAddress = new Uri(_url);
        //    _client.DefaultRequestHeaders.Accept.Clear();
        //    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //}

        //public async Task<ActionResult> SingleTeam()
        //{
        //    HttpResponseMessage response = await _client.GetAsync(_url);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        Team team = await response.Content.ReadAsAsync<Team>();
        //        var teamList = new List<Team> { team };
        //        return View(teamList);
        //    }
        //    return View();
        //}

        //        public async Task<ActionResult> MultipleTeams()
        //        {
        //            HttpResponseMessage response = await _client.GetAsync(_urlPlayers);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        //                RootAPIModel player = await response.Content.ReadAsAsync<RootAPIModel>();
        //                var playerList = new List<RootAPIModel> { player };
        //                return View(playerList);
        //            }
        //            return View();
        //        }

        //Hosted web API REST Service base url  
        string Baseurl = "https://www.balldontlie.io/api/v1/";

        public async Task<ActionResult> Index()
        {
            RootAPIModel playerInfo = new RootAPIModel();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("players");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var PlayerResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    playerInfo = JsonConvert.DeserializeObject<RootAPIModel>(PlayerResponse);

                }
                //returning the employee list to view  
                return View(playerInfo);
            }
        }
    }
}
