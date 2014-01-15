namespace Mashup.Logic.Providers.Search
{
    public class SearchResultItem
    {
        public SearchResultItemMetadata __metadata { get; set; }

        public string ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string DisplayUrl { get; set; }

        public string Url { get; set; }
    }
}