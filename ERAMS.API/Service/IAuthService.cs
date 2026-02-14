using ERAMS.API.Dto;

namespace ERAMS.API.Service
{
    public interface IAuthService
    {
        Task Register(RegisterRequest register);
        Task<string> LogIn(LoginRequest request);


    }
}
