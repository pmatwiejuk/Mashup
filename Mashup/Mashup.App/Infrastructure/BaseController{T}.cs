namespace Mashup.App.Infrastructure
{
    using System.Web.Mvc;

    public class BaseController<TService> : Controller
        where TService : class
    {
        /// <summary>
        /// Konstruktor kontrolera bazowego
        /// </summary>
        /// <param name="service">Serwis danych</param>
        public BaseController(TService service)
        {
            this.Service = service;
        }

        /// <summary>
        /// Serwis danych
        /// </summary>
        public TService Service { get; set; }
    }
}