using System.Collections.Generic;
using Mashup.Logic.Providers.Weather;

namespace Mashup.Logic.Logic
{
    using System;
    using System.Linq;

    using Mashup.Data.Context;
    using Mashup.Data.Model.dbo;
    using Mashup.Logic.Contracts;

    public class WeatherService : IWeatherService
    {
        private WeatherProvider provider;

        public WeatherService()
        {
            this.provider = new WeatherProvider("xkq544hkar4m69qujdgujn7w");
        }

        public List<LocalWeather> LocalWeather(string userMail)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var user = context.Table<Users>().NewQuery().SingleOrDefault(x => x.Email == userMail);

                    if (user == null)
                    {
                        return new List<LocalWeather>();
                    }

                    var weathers = context.Table<Mashup.Data.Model.Weather>().NewQuery().Where(x => x.ID_user == user.ID).ToList();

                    return weathers.Select(x => provider.LocalWeather(x.City, x.Quantity)).ToList();
                }
            }
            catch (Exception)
            {
                //ojojojo
                return null;
            }
        }

        public bool AddCity(string email, string city, int quantity)
        {
            var userId = this.GetUserId(email);
            try
            {
                using (var context = new DataContext())
                {
                    var weatherElement = context.Table<Mashup.Data.Model.Weather>().NewRow();
                    weatherElement.ID_user = userId;
                    weatherElement.City = city;
                    weatherElement.Quantity = quantity;

                    context.Table<Mashup.Data.Model.Weather>().Insert(weatherElement);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                //ojojojo
                throw;
            }

            return false;
        }

        /// <summary>
        /// Metoda pobierająca id użytkownika na podstawie jego maila
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int GetUserId(string email)
        {
            using (var context = new DataContext())
            {
                return context.Table<Users>().NewQuery().Where(x => x.Email == email).Select(x => x.ID).FirstOrDefault();
            }
        }

        public bool RemoveCity(string email, int cityId)
        {
            var userId = this.GetUserId(email);
            try
            {
                using (var context = new DataContext())
                {
                    var weatherElement =
                        context.Table<Mashup.Data.Model.Weather>().NewQuery().FirstOrDefault(x => x.ID_user == userId && x.ID == cityId);
                    if (weatherElement != null)
                    {
                        context.Table<Mashup.Data.Model.Weather>().Delete(weatherElement);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception)
            {
                //ojojojo
                throw;
            }

            return false;
        }
    }
}