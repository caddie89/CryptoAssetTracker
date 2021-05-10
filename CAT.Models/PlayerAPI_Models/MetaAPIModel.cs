using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.PlayerAPI_Models
{
    public class MetaAPIModel
    {
        [JsonProperty("total_pages")]
        public int total_pages { get; set; }
        [JsonProperty("current_page")]
        public int current_page { get; set; }
        [JsonProperty("next_page")]
        public int next_page { get; set; }
        [JsonProperty("per_page")]
        public int per_page { get; set; }
        [JsonProperty("total_count")]
        public int total_count { get; set; }
    }
}
