using CAT.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CAT.WebMVC.Controllers
{
    public class TeamController : Controller
    {
        private readonly HttpClient _client;

        private readonly string _url = "https://www.balldontlie.io/api/v1/teams/1";

        public TeamController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> ConsumeExternalAPI()
        {
            HttpResponseMessage response = await _client.GetAsync(_url);
            if (response.IsSuccessStatusCode)
            {
                Team team = await response.Content.ReadAsAsync<Team>();
                var teamList = new List<Team> { team };
                //var table = JsonConvert.DeserializeObject<Team>(data);
                //var table = JsonConvert.DeserializeAnonymousType(data, new { Makes = default(Team) }).Makes;

                //GridView gView = new GridView();
                //gView.DataSource = table;
                //gView.DataBind();
                //using (StringWriter sw = new StringWriter())
                //{
                //    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                //    {
                //        gView.RenderControl(htw);
                //        ViewBag.ReturnedData = sw.ToString();
                //    }
                //}
                return View(teamList);
            }
            return View();
        }
    }
}

//sonObjectAttribute can also be added to the type to force it to deserialize from a JSON object.

//    private readonly HttpClient _client;

//    private readonly string _url = "https://www.balldontlie.io/api/v1/teams/1";

//    public TeamController()
//    {
//        _client = new HttpClient();
//        _client.BaseAddress = new Uri(_url);
//        _client.DefaultRequestHeaders.Accept.Clear();
//        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//    }

//    public async Task<ActionResult> Index()
//    {
//        HttpResponseMessage responseMessage = await _client.GetAsync(_url);
//        if (responseMessage.IsSuccessStatusCode)
//        {
//            var responseData = responseMessage.Content.ReadAsStringAsync().Result;
//            var result = JsonConvert.DeserializeObject<List<Dictionary<string, Dictionary<string, string>>>>(responseData);
//            //var myDeserializedClass = JsonConvert.DeserializeObject<List<TeamJson>>(responseData);
//            return View(result);
//        }
//        return View("Error");
//    }
//}
