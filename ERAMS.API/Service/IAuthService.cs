namespace ERAMS.API.Service
{
    public interface IAuthService
    {
        Task Register(string username, string email, string password, int roleid);
        Task<string> LogIn(string email, string password);


    }
}
