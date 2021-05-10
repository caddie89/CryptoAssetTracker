using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace CAT.WebMVC.Controllers.PlayerAPI
{
    public class PlayerAPIController : ApiController
    {
        private readonly HttpClient _client;
        private readonly string _url = "https://www.balldontlie.io/api/v1/players/<ID>";
        private readonly string _urlTeams = "https://www.balldontlie.io/api/v1/players";

        public PlayerAPIController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

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

        public async Task<ActionResult> MultipleTeams()
        {
            HttpResponseMessage response = await _client.GetAsync(_urlTeams);
            if (response.IsSuccessStatusCode)
            {
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
                Root teams = await response.Content.ReadAsAsync<Root>();
                var teamsList = new List<Root> { teams };
                return View(teamsList);
            }
            return View();
        }
    }
}
