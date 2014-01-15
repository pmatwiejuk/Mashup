namespace Mashup.Logic.Contracts
{
    using System.Collections.Generic;
    using System.ServiceModel.Syndication;

    using Mashup.Data.Model;

    public interface IRSSService
    {
        List<RSSModel> GetAllRssFeeds(string userName, int? numberPerFeed = null);

        bool RemoveFeed(string name, int feedId);

        bool AddFeed(string email, string feedUrl);
    }
}