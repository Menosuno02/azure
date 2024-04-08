using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFLogicaEscenasPelis.Models;
using WCFLogicaEscenasPelis.Repositories;

namespace WCFLogicaEscenasPelis
{
    public class EscenasClass : IEscenasContract
    {
        private RepositoryEscenas repo;

        public EscenasClass()
        {
            this.repo = new RepositoryEscenas();
        }

        public List<EscenaPelicula> GetEscenas()
        {
            return this.repo.GetEscenas();
        }

        public List<EscenaPelicula> GetEscenasPelicula(int idpelicula)
        {
            return this.repo.GetEscenasPelicula(idpelicula);
        }
    }
}
