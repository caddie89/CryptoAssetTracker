using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.PlayerAPI_Models
{
    public class RootAPIModel
    {
        public List<PlayerAPIModel> data { get; set; }
        public MetaAPIModel meta { get; set; }
    }
}
