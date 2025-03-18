using CAPA.APP.Interfaces.Respositorio;
using CAPA.APP.Interfaces.Servicios;
using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAPA.APP.Servicios
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio; // POR SI VE ESTO, YO PREFIERO USAR IunitOfWork pero por el tiempo y esto es solo de prueba no lo implemento
        private readonly IJwtServices _jwtServices;
        public UsuarioServices(IJwtServices _jwtServices, IUsuarioRepositorio _usuarioRepositorio)
        {
            this._jwtServices = _jwtServices;
            this._usuarioRepositorio = _usuarioRepositorio;
        }

        public ResponseDto<List<UsuarioDto>> GetAll()
        {
            var respose = new ResponseDto<List<UsuarioDto>>();

            respose.Data = _usuarioRepositorio.GetAll().Select(x => new UsuarioDto()
            {
                Email = x.Email,
                Id = x.Id,
                Password = x.Password,
                UserName = x.UserName,
            }).ToList();

            return respose;
        }

        public ResponseDto<AuthResponse> AutenticarLoguin(AuthDto request, ref string mensajeError)
        {
            var result = new ResponseDto<AuthResponse>();

            if(request is null)
            {
                result.TieneError = true;
                result.Mensaje = "No existe información para procesar";
                return result;
            }
            
            if (request.IsValid(ref mensajeError))
            {
                result.TieneError = true;
                result.Mensaje = mensajeError;
                return result;
            }

            var usuarioData = _usuarioRepositorio.GetUsuario(request.UserName, request.PassWord);

            if (usuarioData.TieneError)
            {
                mensajeError = usuarioData.Mensaje;
                return null;
            }

            result.Data = _jwtServices.GenerateToken(usuarioData.Data);

            return result;
        }

        public ResponseDto<bool> ValidCredenciales(string UserName, string Password)
        {
            var respose = new ResponseDto<bool>();

            var usuarioData = _usuarioRepositorio.GetUsuario(UserName, Password);

            if(usuarioData is null)
            {
                respose.TieneError = true;
                respose.Data = false;
                respose.Mensaje = "Nombre de usuario o contraseña no válidos";
                return respose;
            }

            respose.Data = true;
            return respose;
        }
    }
}
