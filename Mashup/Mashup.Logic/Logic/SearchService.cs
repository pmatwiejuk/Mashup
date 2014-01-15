namespace Mashup.Logic.Logic
{
    using System.Collections.Generic;

    using Mashup.Logic.Contracts;
    using Mashup.Logic.Providers.Search;

    public class SearchService : ISearchService
    {
        private const string ApiKey = "j1xHFkbLvgzR/WZXbC/dTp6kl7dv6FKOEa3B1ejH7Qo";

        private SearchProvider provider;

        public SearchService()
        {
            this.provider = new SearchProvider(ApiKey);
        }

        public List<SearchResultItem> Search(string query)
        {
            return provider.Search(query);
        }
    }
}