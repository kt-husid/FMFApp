using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Web.Http;
using System.Xml;
using Kthusid.Web;
using System.Web;

namespace BootstrapWebApplication.Controllers.API
{
    public class RSSController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string url, string type = null)
        {
            //string url = urls[urlId].StartsWith("http://") ? urls[urlId] : "http://" + urls[urlId];
            //var url = String.Empty;
            var newsItems = new List<NewsItem>();
            try
            {
                var r = XmlReader.Create(url);
                var d = SyndicationFeed.Load(r);
                foreach (SyndicationItem item in d.Items)
                {
                    newsItems.Add(new NewsItem()
                    {
                        Date = item.PublishDate.DateTime.ToShortDateString(),
                        Title = item.Title.Text,
                        Content = HttpUtility.HtmlDecode(item.Summary.Text)
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
            //return newsItems;
            return Request.GetResponse(newsItems, type);
        }

        public class NewsItem
        {
            public string Date { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }
    }
}
