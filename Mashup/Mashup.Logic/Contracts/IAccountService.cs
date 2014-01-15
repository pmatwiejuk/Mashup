namespace Mashup.Logic.Contracts
{
    using Mashup.Data.Model.dbo;

    public interface IAccountService
    {
        bool ValidateUser(string email, string password);

        Users GetUserInfo(string email);

        bool SaveUser(string email, Users result);
    }
}