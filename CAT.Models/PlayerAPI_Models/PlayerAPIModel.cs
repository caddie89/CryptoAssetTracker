using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.PlayerAPI_Models
{
    public class PlayerAPIModel
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("position")]
        public string Position { get; set; }
        [JsonProperty("heigh_feet")]
        public int HeightFeet { get; set; }
        [JsonProperty("height_inches")]
        public int HeightInches { get; set; }
        [JsonProperty("weight_pounds")]
        public int WeightLbs { get; set; }
        [JsonProperty("id")]
        public TeamAPIModel Team { get; set; }
    }
}
