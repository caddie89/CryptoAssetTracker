using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Data.Entities
{
    public class TeamJson
    {
        [JsonProperty("data")]
        public Team Team { get; set; }
    }
}
