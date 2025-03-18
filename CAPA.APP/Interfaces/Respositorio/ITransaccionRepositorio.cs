using CAPA.DOMAIN.Entity;
using System.Collections.Generic;

namespace CAPA.APP.Interfaces.Respositorio
{
    public interface ITransaccionRepositorio
    {
        List<Transaccion> GetAll();
        bool Add(Transaccion table);
    }
}
