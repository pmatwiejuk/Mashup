using System.Collections.Generic;

namespace Mashup.Data.Model
{
    public class RSSModel
    {
        public RSSModel()
        {
            this.RssItems = new List<RSSItemModel>();
        }

        public string RSSName { get; set; }

        public string RSSDescription { get; set; }

        public List<RSSItemModel> RssItems { get; set; }
    }
}