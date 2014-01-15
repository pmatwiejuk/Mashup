using System.Web.Mvc;

namespace Mashup.App.Controllers
{
    using System.Collections.Generic;

    using Mashup.App.Infrastructure;
    using Mashup.Data.Model;
    using Mashup.Logic.Contracts;
    using Mashup.Logic.Logic;

    [Authorize]
    public class RSSController : BaseController<IRSSService>
    {
        public RSSController()
            : base(new RSSService())
        {
        }

        [ChildActionOnly]
        public ActionResult Index()
        {
            List<RSSModel> rss = this.Service.GetAllRssFeeds(this.User.Identity.Name, 10);
            return this.PartialView(rss);
        }

        public ActionResult RemoveFeed(int feedId)
        {
            var removeSuccess = this.Service.RemoveFeed(this.User.Identity.Name, feedId);
            if (!removeSuccess)
            {
                this.ModelState.AddModelError("FeedError", "Wystąpił problem w trakcie usuwania źródła");
                this.TempData["ViewData"] = this.ViewData;
            }

            return this.RedirectToAction("EditProfile", "Home");
        }

        [HttpPost]
        public JsonResult AddFeed(string feedUrl)
        {
            var addSuccess = this.Service.AddFeed(this.User.Identity.Name, feedUrl);
            if (!addSuccess)
            {
                this.ModelState.AddModelError("FeedError", "Wystąpił problem w trakcie dodawania źródła");
                this.TempData["ViewData"] = this.ViewData;
            }

            return this.Json(addSuccess);
        }
    }
}