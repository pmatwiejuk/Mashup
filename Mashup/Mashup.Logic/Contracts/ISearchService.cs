namespace Mashup.Logic.Contracts
{
    using System.Collections.Generic;

    using Mashup.Logic.Providers.Search;

    public interface ISearchService
    {
        List<SearchResultItem> Search(string query);
    }
}