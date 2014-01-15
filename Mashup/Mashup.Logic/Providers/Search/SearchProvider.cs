namespace Mashup.Logic.Providers.Search
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    using Newtonsoft.Json;

    public class SearchProvider
    {
        private const string ApiUrl = "https://api.datamarket.azure.com/Data.ashx/Bing/Search/v1/Web";

        private readonly string apiKey;
    
        public SearchProvider(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public List<SearchResultItem> Search(string query)
        {
            var res = this.Get<SearchResult>(new { Query = string.Format("%27{0}%27", query) });
            return res.d.results;
        }

        private TResult Get<TResult>(dynamic request)
            where TResult : class
        {
            var searchUrl = new StringBuilder(ApiUrl);
            searchUrl.Append("?$format=JSON&$top=10");

            Type type = request.GetType();
            foreach (var prop in type.GetProperties())
            {
                var name = prop.Name;
                var value = prop.GetValue(request);

                searchUrl.Append(string.Format("&{0}={1}", name, value));
            }

            var wc = new WebClient { Credentials = new NetworkCredential(this.apiKey, this.apiKey) };
            var result = wc.DownloadString(new Uri(searchUrl.ToString()));

            var res = JsonConvert.DeserializeObject<TResult>(result);
            return res;
        }
    }
}
