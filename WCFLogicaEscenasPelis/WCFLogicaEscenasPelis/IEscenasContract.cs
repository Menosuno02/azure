using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFLogicaEscenasPelis.Models;

namespace WCFLogicaEscenasPelis
{
    [ServiceContract]
    public interface IEscenasContract
    {
        [OperationContract]
        List<EscenaPelicula> GetEscenas();
        [OperationContract]
        List<EscenaPelicula> GetEscenasPelicula(int idpelicula);
    }
}
