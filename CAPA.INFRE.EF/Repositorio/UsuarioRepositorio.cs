using CAPA.APP.Interfaces.Respositorio;
using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.Entity;
using NUGET.TOOL.CORE._3_1.AES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CAPA.INFRE.EF.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AppDbContext _db;

        private readonly IAesEncryption aes;

        public UsuarioRepositorio(AppDbContext _db, IAesEncryption aes)
        {
            this._db = _db;
            this.aes = aes;
        }

        public List<Usuario> GetAll()
            => _db.Usuario.Where(x=> x.Estado==true).ToList();
        

        public Usuario GetUsuario(string UserName, string Passs)
            => _db.Usuario.FirstOrDefault(x => x.UserName.Equals(UserName, StringComparison.OrdinalIgnoreCase) &&
                                   x.UserName.Equals(UserName, StringComparison.OrdinalIgnoreCase));
        


        ResponseDto<Usuario> IUsuarioRepositorio.GetUsuario(string UserName, string Passs)
        {
            throw new NotImplementedException();
        }
    }
}
