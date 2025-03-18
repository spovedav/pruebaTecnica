using CAPA.APP.Interfaces.Respositorio;
using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.Entity;
using CAPA.INFRE.EF;
using Dapper;
using NUGET.TOOL.CORE._3_1.AES;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace CAPA.INFRE.Respositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AppDbContext context;
        private readonly IAesEncryption aes;

        public UsuarioRepositorio(AppDbContext db, IAesEncryption aes)
        {
            this.context = db;
            this.aes = aes;
        }

        public List<Usuario> GetAll()
        {
            return Datos();
        }

        public ResponseDto<Usuario> GetUsuario(string UserName, string Passs)
        {
            var modelo = new ResponseDto<Usuario>();

            var datos = Datos();

            var usuario = datos.Where(x => x.UserName.Equals(UserName, StringComparison.OrdinalIgnoreCase) && x.Estado == true)
                .FirstOrDefault();

            if (usuario is null)
            {
                modelo.TieneError = true;
                modelo.Mensaje = "El usuario no exite";
                return modelo;
            }

            string CalveEncriptada = aes.Encrypt(Passs);

            if (usuario.Password != CalveEncriptada) { 
                modelo.TieneError = true;
                modelo.Mensaje = "Usuario ó Clave Incorrecta";
                return modelo;
            }

            modelo.Data = usuario;

            return modelo;
        }


        private List<Usuario> Datos()
        {
            var lista = context.Usuario.ToList();

            return lista;
        }

    }
}
