using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mashup.Logic.Providers.Weather
{
    
        public class LocalWeather
        {
            public Data data;
        }

        public class Data
        {
            public List<Current_Condition> current_Condition;
            public List<Request> request;
            public List<Weather> weather;
        }

        public class Current_Condition
        {
            public DateTime observation_time { get; set; }
            public DateTime localObsDateTime { get; set; }
            public int temp_C { get; set; }
            public int windspeedMiles { get; set; }
            public int windspeedKmph { get; set; }
            public int winddirDegree { get; set; }
            public string winddir16Point { get; set; }
            public string weatherCode { get; set; }
            public List<WeatherDesc> weatherDesc { get; set; }
            public List<WeatherIconUrl> weatherIconUrl { get; set; }
            public float precipMM { get; set; }
            public float humidity { get; set; }
            public int visibility { get; set; }
            public int pressure { get; set; }
            public int cloudcover { get; set; }
        }

        public class Request
        {
            public string query { get; set; }
            public string type { get; set; }
        }

        public class Weather
        {
            public DateTime date { get; set; }
            public int tempMaxC { get; set; }
            public int tempMaxF { get; set; }
            public int tempMinC { get; set; }
            public int tempMinF { get; set; }
            public int windspeedMiles { get; set; }
            public int windspeedKmph { get; set; }
            public int winddirDegree { get; set; }
            public string winddir16Point { get; set; }
            public string weatherCode { get; set; }
            public List<WeatherDesc> weatherDesc { get; set; }
            public List<WeatherIconUrl> weatherIconUrl { get; set; }
            public float precipMM { get; set; }
        }

        public class WeatherDesc
        {
            public string value { get; set; }
        }

        public class WeatherIconUrl
        {
            public string value { get; set; }
        }
}

