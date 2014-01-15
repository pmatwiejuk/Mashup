namespace Mashup.Logic.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Xml;

    using Mashup.Data.Context;
    using Mashup.Data.Model;
    using Mashup.Data.Model.dbo;
    using Mashup.Logic.Contracts;

    public class RSSService : IRSSService
    {
        /// <summary>
        /// Metoda pobierająca id użytkownika na podstawie jego maila
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int GetUserId(string email)
        {
            using (var context = new DataContext())
            {
                return context.Table<Users>().NewQuery().Where(x => x.Email == email).Select(x => x.ID).FirstOrDefault();
            }
        }

        /// <summary>
        /// Metoda pobierająca wszystkie feedy
        /// </summary>
        /// <param name="userName">Nazwa zalogowanego uzytkownika</param>
        /// <param name="numberPerFeed">ilość per feed</param>
        /// <returns></returns>
        public List<RSSModel> GetAllRssFeeds(string userName, int? numberPerFeed = null)
        {
            var userId = this.GetUserId(userName);
            var result = new List<RSSModel>();
            try
            {
                using (var context = new DataContext())
                {
                    var feedUrls =
                        context.Table<RSS>().NewQuery().Where(x => x.ID_user == userId).Select(x => x.RSS_URL).ToList();

                    foreach (var url in feedUrls)
                    {
                        var subResult = new RSSModel();

                        XmlReader rssXml;
                        try
                        {
                            rssXml = XmlReader.Create(url);
                        }
                        catch (Exception)
                        {
                            break;
                        }
                        var rss = SyndicationFeed.Load(rssXml);
                        if (rss != null)
                        {
                            subResult.RSSName = rss.Title.Text;
                            subResult.RSSDescription = rss.Description.Text;

                            if (numberPerFeed != null)
                            {
                                rss.Items = rss.Items.OrderBy(x => x.PublishDate.DateTime).Take((int)numberPerFeed).ToList();
                            }

                            foreach (var item in rss.Items)
                            {
                                var rssItemSubResult = new RSSItemModel
                                                           {
                                                               Title = item.Title.Text,
                                                               PublishDate = item.PublishDate.DateTime,
                                                               Content = item.Summary.Text
                                                           };

                                var uri = item.Links.Select(x => x.Uri).FirstOrDefault();
                                if (uri != null)
                                {
                                    rssItemSubResult.Url = uri.AbsoluteUri;
                                }

                                subResult.RssItems.Add(rssItemSubResult);
                            }
                        }

                        result.Add(subResult);
                    }

                    return result;
                }
            }
            catch (Exception)
            {
                // ojojojoj
            }

            return null;
        }

        /// <summary>
        /// Metoda usuwająca feed
        /// </summary>
        /// <param name="name"></param>
        /// <param name="feedId"></param>
        /// <returns></returns>
        public bool RemoveFeed(string name, int feedId)
        {
            var userId = this.GetUserId(name);
            try
            {
                using (var context = new DataContext())
                {
                    var feedElement =
                        context.Table<RSS>().NewQuery().FirstOrDefault(x => x.ID_user == userId && x.ID == feedId);
                    if (feedElement != null)
                    {
                        context.Table<RSS>().Delete(feedElement);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception)
            {
                //ojojojo
            }

            return false;
        }

        /// <summary>
        /// Metoda dodająca nowy feed
        /// </summary>
        /// <param name="email"></param>
        /// <param name="feedUrl"></param>
        /// <returns></returns>
        public bool AddFeed(string email, string feedUrl)
        {
            var userId = this.GetUserId(email);
            try
            {
                using (var context = new DataContext())
                {
                    var feedElement = context.Table<RSS>().NewRow();
                    feedElement.ID_user = userId;
                    feedElement.RSS_URL = feedUrl;

                    context.Table<RSS>().Insert(feedElement);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                //ojojojo
            }

            return false;
        }

    }
}