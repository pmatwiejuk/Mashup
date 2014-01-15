using System.Linq;
using System.Web.Mvc;
using Mashup.App.Infrastructure;
using Mashup.Logic.Contracts;
using Mashup.Logic.Logic;

namespace Mashup.App.Controllers
{
    public class WeatherController : BaseController<IWeatherService>
    {
        //
        // GET: /Weather/

        public WeatherController() : base(new WeatherService())
        {

        }

        [ChildActionOnly]
        public ActionResult Index()
        {
            var result = this.Service.LocalWeather(User.Identity.Name).Where(x => x.data.request != null).ToList();

            return this.PartialView(result);
        }

        public ActionResult AddCity(string city, int quantity)
        {
            var addSuccess = this.Service.AddCity(this.User.Identity.Name, city, quantity);
            if (!addSuccess)
            {
                this.ModelState.AddModelError("WeatherError", "Wystąpił problem w trakcie dodawania miasta");
                this.TempData["ViewData"] = this.ViewData;
            }

            return this.Json(addSuccess);
        }

        public ActionResult RemoveCity(int cityId)
        {
            var removeSuccess = this.Service.RemoveCity(this.User.Identity.Name, cityId);
            if (!removeSuccess)
            {
                this.ModelState.AddModelError("WeatherError", "Wystąpił problem w trakcie usuwania miasta");
                this.TempData["ViewData"] = this.ViewData;
            }

            return this.RedirectToAction("EditProfile", "Home");
        }
    }
}
