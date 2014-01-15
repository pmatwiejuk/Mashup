namespace Mashup.App.Controllers
{
    using System.Web.Mvc;

    using Mashup.App.Infrastructure;
    using Mashup.Logic.Contracts;
    using Mashup.Logic.Logic;

    public class SearchController : BaseController<ISearchService>
    {
        public SearchController()
            : base(new SearchService())
        {
        }

        [ChildActionOnly]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            var result = this.Service.Search(query);
            return this.PartialView(result);
        }
    }
}