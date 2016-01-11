using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UniversalSx
{
    internal class StackExchangeSite
    {

        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "api_site_parameter")]
        public string ApiName { get; private set; }

        [JsonProperty(PropertyName = "site_url")]
        public string SiteUrl { get; private set; }
    }
}
