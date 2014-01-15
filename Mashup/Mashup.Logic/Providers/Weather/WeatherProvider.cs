using Newtonsoft.Json;
using System;
using System.Net;

namespace Mashup.Logic.Providers.Weather
{

    internal class WeatherProvider
    {
        private const string APIurl = "http://api.worldweatheronline.com/free/v1/";
        private string apiKey;

        public WeatherProvider(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public LocalWeather LocalWeather(string city, int quantity)
        {
            var loc = this.Get<LocalWeather>(new {format = "json", num_of_days = quantity, key = apiKey, q = city});

            return loc;

        }

        private TResponse Get<TResponse>(dynamic request)
            where TResponse : class
        {
            string apiURL = APIurl + "weather.ashx";

            var reqType = request.GetType();
            var properties = reqType.GetProperties();
            bool first = true;
            foreach (var prop in properties)
            {
                var name = prop.Name;
                var value = prop.GetValue(request);

                apiURL += string.Format("{0}{1}={2}", first ? "?" : "&", name, value);
                first = false;

            }

            var webclient = new WebClient();
            var result = webclient.DownloadString(new Uri(apiURL));

            var loc = JsonConvert.DeserializeObject<TResponse>(result);
            return loc;
        }
    }
}