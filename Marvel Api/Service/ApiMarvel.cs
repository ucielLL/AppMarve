using Marvel_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Marvel_Api.Service
{
   public class ApiMarvel: IApiMarvel
    { 
      
        HttpClient Client = new();
        public ApiMarvel()
        {
                Client.BaseAddress = new Uri(Constants.ApiRoot); 
        }
        const string COMICS = "comics?";
        const string SERIES = "series?";
        async Task<List<ItemBase>> Get(string request,OptionFilters optionfilters = null) 
        {
            List<ItemBase> itemResult = null;
            var requestUri = request;
            if (optionfilters != null)
            {
                requestUri += optionfilters.GetFilter() + "&";
            }
            HttpResponseMessage response = await Client.GetAsync(requestUri + Constants.Key);
            if (response.IsSuccessStatusCode)
            {
                var root = await response.Content.ReadFromJsonAsync<RootResponse>();
                itemResult = root.data.results.Select(result =>
                {
                    var i = JsonSerializer.Deserialize<ItemResultBase>(result.ToString());
                    i.thumbnailPath = i.thumbnail != null ? i.thumbnail.path.Replace("http", "https") + "." + i.thumbnail.extension : "";
                    return i as ItemBase;   
                    
                }).ToList();
            }
            return itemResult;
        }
        public async Task<List<ItemBase>> GetComics( OptionFilters optionfilters = null) 
        {
            var result = await Get(COMICS, optionfilters);
            return result; 
        }
        public async Task<List<ItemBase>> GetSeries(OptionFilters optionfilters = null) 
        {
            var result = await Get(SERIES, optionfilters);
            return result;
        }
        public async Task<ItemResultBase> GetComicById(int Id) 
        {
            if (Id <= 0) return null;
            HttpResponseMessage response = await Client.GetAsync("comics/" + Id + "?"+ Constants.Key);

            if (response.IsSuccessStatusCode) 
            {
             var root = await response.Content.ReadFromJsonAsync<RootResponse>();
               ItemResultBase result = JsonSerializer.Deserialize<ItemResultBase>(root.data.results.FirstOrDefault().ToString());
                result.thumbnailPath = result.thumbnail != null ? result.thumbnail.path.Replace("http", "https") + "." + result.thumbnail.extension : "";
                return result;
            }
            return null;    
        }
        public async Task<ItemResultSeries> GetSerieById(int Id) 
        {
            if (Id <= 0) return null;
           
            HttpResponseMessage response = await Client.GetAsync("series/" + Id + "?" + Constants.Key);
            if (response.IsSuccessStatusCode)
            {
                var root = await response.Content.ReadFromJsonAsync<RootResponse>();
                ItemResultSeries result = JsonSerializer.Deserialize<ItemResultSeries>(root.data.results.FirstOrDefault().ToString());
                result.thumbnailPath = result.thumbnail != null ? result.thumbnail.path.Replace("http", "https") + "." + result.thumbnail.extension : "";
                return result;
            }
            return null;
        }
        public async Task<ItemCharacter> GetCharacterWhitUri(string uri) 
        {
            if (uri == null) return null;
            uri = uri.Replace("http", "https");
            HttpResponseMessage response = await Client.GetAsync(uri + "?"+ Constants.Key);
            if (response.IsSuccessStatusCode) 
            {
                var root = await response.Content.ReadFromJsonAsync<RootResponse>();
                return JsonSerializer.Deserialize<ItemCharacter>(root.data.results.FirstOrDefault().ToString());     
               
            }
            return null;    

        }
    }
}
