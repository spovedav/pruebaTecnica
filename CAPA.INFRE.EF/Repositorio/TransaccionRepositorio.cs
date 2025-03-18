using CAPA.APP.Interfaces.Respositorio;
using CAPA.DOMAIN.Entity;
using System.Collections.Generic;
using System.Linq;

namespace CAPA.INFRE.EF.Repositorio
{
    public class TransaccionRepositorio : ITransaccionRepositorio
    {
        private readonly AppDbContext _appDbContext;
        public TransaccionRepositorio(AppDbContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }

        public List<Transaccion> GetAll()
            => _appDbContext.Transacciones.Where(x => x.Estado == true).ToList();

        public bool Add(Transaccion table)
        {
            _appDbContext.Transacciones.Add(table);
            return _appDbContext.SaveChanges() > 0;
        }
    }
}
