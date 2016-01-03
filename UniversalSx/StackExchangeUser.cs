using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UniversalSx
{
    
    
    internal class StackExchangeUser
    {
        [JsonProperty(PropertyName = "user_id")]
        public int SiteUserId { get; private set; }

        [JsonProperty(PropertyName = "display_name")]
        public string UserName { get; private set; }

        [JsonProperty(PropertyName = "reputation")]
        public int Reputation { get; private set; }

        [JsonProperty(PropertyName = "account_id")]
        public int AccountId { get; private set; }

        [JsonProperty(PropertyName = "site_name")]
        public string SiteName { get; private set; }

        public StackExchangeUser(int siteUserId, String userName, int reputation, int accountId, string siteName)
        {
            SiteUserId = siteUserId;
            UserName = userName;
            Reputation = reputation;
            AccountId = accountId;
            SiteName = siteName;
        }

        public StackExchangeUser()
        {
        }


        public string Summary => (SiteName ?? "") + ": " + Reputation;
    }
}
