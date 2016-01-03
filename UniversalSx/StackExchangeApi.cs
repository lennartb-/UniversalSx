using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UniversalSx
{
    internal class StackExchangeApi
    {
        private readonly string publicAuthKey;
        private const string ApiVersion = "2.2";
        private const string RequestUrl = "https://api.stackexchange.com/" + ApiVersion;
        private const int ClientId = 6224;
        internal static string PublicApiKey = "DTIn*vHbk8p3wgk0adgX3w((";


        public StackExchangeApi(string publicAuthKey)
        {
            this.publicAuthKey = publicAuthKey;
        }

        public async Task<IEnumerable<StackExchangeUser>> GetNetworkUser(int id)
        {
            return await GetStackExchangeObject<StackExchangeUser>(String.Format("/users/{0}", id));
        }

        public async Task<IEnumerable<StackExchangeUser>> GetAssociatedUsers(int id)
        {
            var allAccounts =  await GetStackExchangeObject<StackExchangeUser>(String.Format("/users/{0}/associated", id));

            var stackExchangeUsers = allAccounts as IList<StackExchangeUser> ?? allAccounts.ToList();
            var overallReputation = stackExchangeUsers.Sum(account => account.Reputation);
            var networkProfile = new StackExchangeUser(id,"Overall",overallReputation,id,"Overall");

            var fullList = new List<StackExchangeUser> {networkProfile};
            fullList.AddRange(stackExchangeUsers);
            return fullList;
        }

        private async Task<IEnumerable<T>> GetStackExchangeObject<T>(string path) where T : class, new()
        {
            var requestUri = BuildRequestUri(path);
            var json = await GetResponse(requestUri);
            return ParseJson<T>(json);
        }

        private string BuildRequestUri(string path)
        {
            var uri = RequestUrl + path;
            return String.Format("{0}?key={1}", uri, publicAuthKey);
        }

        private async Task<String> GetResponse(string requestUri)
        {
            var request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Accept = "application/json";
            var result = await request.GetResponseAsync();
            var json = ExtractJsonResponse(result);
            return json;
        }

        private static IEnumerable<T> ParseJson<T>(string json) where T : class, new()
        {

            var jobject = JObject.Parse(json);
            var collection = JsonConvert.DeserializeObject<List<T>>(jobject["items"].ToString());
            return collection;
           
        }

        private static string ExtractJsonResponse(WebResponse response)
        {
            string json;
            using (var outStream = new MemoryStream())
            using (var zipStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
            {
                zipStream.CopyTo(outStream);
                outStream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(outStream, Encoding.UTF8))
                {
                    json = reader.ReadToEnd();
                }
            }
            return json;
        }
    }
}
