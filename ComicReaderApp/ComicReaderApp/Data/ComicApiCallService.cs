using ComicReaderApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ComicReaderApp.Data
{
    class ComicApiCallService
    {
        public async Task<FolderModel> GetFolderListAsync(int? page = 1)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(UserSettings.ApiLocation)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = $"{client.BaseAddress}?folder=comic&pagelimit={UserSettings.PageLimit}&page={page}&json=true";
            HttpResponseMessage response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            JObject jObject = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
            if (jObject != null)
            {
                FolderModel returnList = new FolderModel(jObject);
                return returnList;
            }
            else
            {
                FolderModel emptyResponseList = new FolderModel();
                return emptyResponseList;
            }
        }
    }
}
