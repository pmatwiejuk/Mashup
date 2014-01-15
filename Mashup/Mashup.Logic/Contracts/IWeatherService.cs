using System.Collections.Generic;
using Mashup.Logic.Providers.Weather;

namespace Mashup.Logic.Contracts
{
    public interface IWeatherService
    {
        List<LocalWeather> LocalWeather(string userMail);

        bool AddCity(string email, string city, int quantity);

        bool RemoveCity(string email, int cityId);
    }
}