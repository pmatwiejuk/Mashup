namespace Mashup.Logic.Logic
{
    using System;
    using System.Data.Entity;

    using Mashup.Data.Context;
    using Mashup.Data.Model.dbo;
    using Mashup.Logic.Contracts;
    using System.Linq;

    public class AccountService : IAccountService
    {
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

        public bool ValidateUser(string email, string password)
        {
            try
            {
                using (var context = new DataContext())
                {
                    return context.Table<Users>().NewQuery().Any(x => x.Email == email && x.Password == password);
                }
            }
            catch (Exception ex)
            {
                // ojojojoj
            }

            return false;
        }

        public Users GetUserInfo(string email)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var user = context.Table<Users>().NewQuery().Include(x => x.Weather).Include(x => x.RSS).FirstOrDefault(x => x.Email == email);
                    return user;
                }
            }
            catch (Exception ex)
            {
                // ojojojoj
            }

            return null;
        }

        /// <summary>
        /// Metoda zapisująca informacje o użytkowniku
        /// </summary>
        /// <param name="email"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool SaveUser(string email, Users result)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var user = context.Table<Users>().NewQuery().FirstOrDefault(x => x.Email == email);
                    if (user != null)
                    {
                        user.Name = result.Name;
                        user.Surname = result.Surname;
                        if (!string.IsNullOrEmpty(result.Password))
                        {
                            user.Password = result.Password;
                        }

                        context.Table<Users>().Update(user);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // ojojojoj
            }

            return false;
        }
    }
}