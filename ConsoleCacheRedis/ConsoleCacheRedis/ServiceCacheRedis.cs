using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCacheRedis
{
    public class ServiceCacheRedis
    {
        private IDatabase database;

        public ServiceCacheRedis()
        {
            database = HelperCacheMultiplexer
                .Connection.GetDatabase();
        }

        // MÉTODO PARA ALMACENAR PRODUCTOS
        public async Task AddProductoFavoritoAsync(Producto producto)
        {
            // CACHE REDIS FUNCIONA CON KEY/VALUE
            // DICHAS CLAVES SON COMUNES PARA TODOS LOS USUARIOS
            // DEBERÍAMOS TENER UNA CLAVE ÚNICA PARA CADA USER
            // VAMOS A ALMACENAR EL PRODUCTO CON FORMATO JSON
            // LO QUE TENDREMOS EN CACHE REDIS SERÁ UNA
            // COLECCIÓN DE TODOS LOS PRODUCTOS
            string jsonProductos = await database.StringGetAsync("favoritos");
            List<Producto> productos;
            if (jsonProductos == null)
            {
                // NO TENEMOS FAVORITOS, CREAMOS LA COLECCIÓN
                productos = new List<Producto>();
            }
            else
            {
                // YA TENEMOS FAVORITOS EN CACHE, LOS RECUPERAMOS
                productos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
            }
            // INCLUIMOS EL NUEVO PRODUCTO
            productos.Add(producto);
            // SERIALIZAMOS LA COLECCIÓN CON LOS NUEVOS DATOS
            jsonProductos = JsonConvert.SerializeObject(productos);
            // ALMACENAMOS LOS NUEVOS DATOS EN AZURE CACHE REDIS
            await database.StringSetAsync("favoritos", jsonProductos);
        }

        // MÉTODO PARA RECUPERAR TODOS LOS PRODUCTOS
        public async Task<List<Producto>> GetProductosFavoritosAsync()
        {
            string jsonProductos = await database.StringGetAsync("favoritos");
            if (jsonProductos == null)
            {
                return null;
            }
            else
            {
                List<Producto> favoritos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
                return favoritos;
            }
        }

        // MÉTODO PARA ELIMINAR PRODUCTOS DE FAVORITOS
        public async Task DeleteProductoFavoritoAsync(int idproducto)
        {
            // ESTO NO ES UNA BASE DE DATOS, NO PODEMOS FILTRAR
            List<Producto> favoritos = await GetProductosFavoritosAsync();
            if (favoritos != null)
            {
                // BUSCAMOS EL PRODUCTO
                Producto productoDelete = favoritos
                    .FirstOrDefault(p => p.IdProducto == idproducto);
                // ELIMINAMOS EL PRODUCTO DE LA COLECCIÓN
                favoritos.Remove(productoDelete);
                // TENEMOS QUE COMPROBAR SI TODAVÍA TENEMOS
                // ALGÚN FAVORITO
                if (favoritos.Count == 0)
                {
                    // SI YA NO TENEMOS FAVORITOS ELIMINAMOS
                    // LA KEY DE AZURE CACHE REDIS
                    await database.KeyDeleteAsync("favoritos");
                }
                else
                {
                    // ALMACENAMOS LOS PRODUCTOS FAVORITOS
                    // DE NUEVO
                    string jsonProductos = JsonConvert.SerializeObject(favoritos);
                    // PODEMOS INDICAR POR CÓDIGO EL TIEMPO DE
                    // DURACIÓN DE LA KEY DENTRO DE CACHE REDIS
                    // SI NO LE DIGO NADA, EL TIEMPO PREDETERMINADO
                    // ES DE 24 HORAS
                    await database.StringSetAsync("favoritos", jsonProductos, TimeSpan.FromMinutes(30));
                }
            }
        }
    }

}
