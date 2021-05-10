using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.PlayerAPI_Models
{
    public class TeamAPIModel
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("abbreviation")]
        public string TeamAbbreviation { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("conference")]
        public string Conference { get; set; }
        [JsonProperty("division")]
        public string Division { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
