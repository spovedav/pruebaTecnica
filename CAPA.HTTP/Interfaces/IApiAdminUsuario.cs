using CAPA.DOMAIN.DTOs;
using System.Collections.Generic;

namespace CAPA.HTTP.Interfaces
{
    public interface IApiAdminUsuario
    {
        ResponseDto<List<UsuarioDto>> GetAll(string Token, object queryParameto = null);
    }
}
