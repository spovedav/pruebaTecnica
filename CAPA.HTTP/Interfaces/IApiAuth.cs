using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.DTOs.Auth;

namespace CAPA.HTTP.Interfaces
{
    public interface IApiAuth
    {
        ResponseDto<AuthResponse> AutenticateGetToken(string UserName, string Password, string Token);
    }
}
