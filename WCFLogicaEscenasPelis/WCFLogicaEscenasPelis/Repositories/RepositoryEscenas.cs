using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WCFLogicaEscenasPelis.Models;

namespace WCFLogicaEscenasPelis.Repositories
{
    public class RepositoryEscenas
    {
        private XDocument document;

        public RepositoryEscenas()
        {
            string resourceName = "WCFLogicaEscenasPelis.escenaspeliculas.xml";
            Stream stream = this.GetType()
                .Assembly.GetManifestResourceStream(resourceName);
            this.document = XDocument.Load(stream);
        }

        public List<EscenaPelicula> GetEscenas()
        {
            var consulta = from datos in
                               document.Descendants("escena")
                           select new EscenaPelicula()
                           {
                               IdPelicula = int.Parse(datos.Attribute("idpelicula").Value),
                               Titulo = datos.Element("tituloescena").Value,
                               Descripcion = datos.Element("descripcion").Value,
                               Imagen = datos.Element("imagen").Value
                           };
            return consulta.ToList();
        }

        public List<EscenaPelicula> GetEscenasPelicula(int idpelicula)
        {
            return this.GetEscenas()
                .Where(e => e.IdPelicula == idpelicula)
                .ToList();
        }
    }
}
