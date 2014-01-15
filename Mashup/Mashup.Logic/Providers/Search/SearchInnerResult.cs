namespace Mashup.Logic.Providers.Search
{
    using System.Collections.Generic;

    public class SearchInnerResult
    {
        public SearchInnerResult()
        {
            this.results = new List<SearchResultItem>();
        }

        public List<SearchResultItem> results { get; set; }
    }
}