using Hospital.Application.DTOs.Authentication;

namespace Hospital.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthDto> Login(UserLoginDto model);
        Task Register(UserRegisterDto model);
    }
}
