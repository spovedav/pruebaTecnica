using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.Entity;
using System.Collections.Generic;

namespace CAPA.APP.Interfaces.Respositorio
{
    //POR SI VE ESTO PODRIA USAR UNA INTERFAS DE IRepository<Table> ... pero es cuestion tiempo
    public interface IUsuarioRepositorio
    {
        // POR SI VE ESTO, SERÍA MÁS RECOMENDALE USAR ASEumerable o AsQuerble para no traer todo en memoria, paginación
        List<Usuario> GetAll();

        ResponseDto<Usuario> GetUsuario(string UserName, string Passs);
    }
}
