using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.Team
{
    public class Root
    {
        [JsonProperty("data")]
        public List<Team> Teams { get; set; }
        public Meta Meta { get; set; }
    }
}
