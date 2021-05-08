using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.Team
{
    public class Team
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("abbreviation")]
        public string Abbreviation { get; set; }

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
