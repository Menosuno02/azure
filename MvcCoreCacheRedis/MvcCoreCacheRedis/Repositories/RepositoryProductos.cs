using MvcCoreCacheRedis.Helpers;
using MvcCoreCacheRedis.Models;
using System.Xml.Linq;

namespace MvcCoreCacheRedis.Repositories
{
    public class RepositoryProductos
    {
        private HelperPathProvider helper;
        private XDocument document;

        public RepositoryProductos(HelperPathProvider helper)
        {
            string path = helper.MapPath("productos.xml", Folders.Documents);
            this.document = XDocument.Load(path);
        }

        public List<Producto> GetProductos()
        {
            var consulta = from datos in this.document.Descendants("producto")
                           select new Producto
                           {
                               IdProducto = int.Parse(datos.Element("idproducto").Value),
                               Nombre = datos.Element("nombre").Value,
                               Descripcion = datos.Element("descripcion").Value,
                               Precio = int.Parse(datos.Element("precio").Value),
                               Imagen = datos.Element("imagen").Value
                           };
            return consulta.ToList();
        }

        public Producto FindProducto(int idproducto)
        {
            return this.GetProductos()
                .FirstOrDefault(p => p.IdProducto == idproducto);
        }
    }
}
