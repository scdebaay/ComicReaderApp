﻿using ComicReaderApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ComicReaderApp.Data
{    
    /// <summary>
    /// Represents calls to the API to fetch a list of comics. Responsible for Deserializing result into Folder object with containing ComicListItems.
    /// </summary>
    class ComicApiCallService
    {
        /// <summary>
        /// Async call to retrieve folder list with available comics. 
        /// Call is made to API location from UserSettings or Default settings.
        /// This is done in batches, delimited by page, using page limit setting from UserSettings or Default Settings.
        /// </summary>
        /// <param name="page">int, defaults to 1, denotes page number to fetch. Folder metadata contains current page and available pages</param>
        /// <returns>Folder Model object containing a list of ComicsListItems for the requested page.</returns>
        public async Task<FolderModel> GetFolderListAsync(int? page = 0)
        {
            return await GetFolderListAsync("", page);
        }

        /// <summary>
        /// Async call to retrieve folder list with available comics. 
        /// Call is made to API location from UserSettings or Default settings.
        /// This is done in batches, delimited by page, using page limit setting from UserSettings or Default Settings.
        /// </summary>
        /// <param name="searchText">string, denotes the search to submit to the API.</param>
        /// <param name="page">int, defaults to 1, denotes page number to fetch. Folder metadata contains current page and available pages</param>
        /// <returns>Folder Model object containing a list of ComicsListItems for the requested page.</returns>
        public async Task<FolderModel> GetFolderListAsync( string searchText, int? page = 0)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(UserSettings.ApiLocation)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = $"{client.BaseAddress}folder/comic/{page}?pagelimit={UserSettings.PageLimit}&searchText={searchText}";
            HttpResponseMessage response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            JObject jObject = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
            if (jObject != null)
            {
                FolderModel returnList = new FolderModel(jObject);
                client.Dispose();
                return returnList;
            }
            else
            {
                FolderModel emptyResponseList = new FolderModel();
                client.Dispose();
                return emptyResponseList;
            }
        }

        /// <summary>
        /// Async call to retrieve api version data. 
        /// Call is made to API location from Settings VM.
        /// </summary>
        /// <returns>Api Version Model object containing version data.</returns>
        public async Task<ApiVersionModel> GetApiVersionAsync()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(UserSettings.ApiLocation)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = $"{client.BaseAddress}Version";
            HttpResponseMessage response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            ApiVersionModel versionResult = JsonConvert.DeserializeObject<ApiVersionModel>(response.Content.ReadAsStringAsync().Result);
            return versionResult;
        }
    }
}
