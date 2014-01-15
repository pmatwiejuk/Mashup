namespace Mashup.Data.Model
{
    using System;

    public class RSSItemModel
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime PublishDate { get; set; }
        public string Content { get; set; }
    }
}