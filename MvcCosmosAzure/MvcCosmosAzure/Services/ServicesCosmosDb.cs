using Microsoft.Azure.Cosmos;
using MvcCosmosAzure.Models;

namespace MvcCosmosAzure.Services
{
    public class ServiceCosmosDb
    {
        //DENTRO DE COSMOS TRABAJAMOS CON CONTAINERS 
        //Y DENTRO DEL CONTAINER ESTARAN LOS ITEMS
        //DESDE ESTE CODIGO VAMOS A CREAR UN CONTAINER
        //DEBEMOS RECIBIR CosmosClient
        private CosmosClient clientCosmos;
        private Container containerCosmos;
        public ServiceCosmosDb(CosmosClient client,
            Container container)
        {
            this.clientCosmos = client;
            this.containerCosmos = container;
        }

        //VAMOS A CREAR UN METODO PARA CREAR NUESTRA BASE DE DATOS
        //Y DENTRO DE LA BASE DE DATOS NUESTRO CONTENEDOR
        public async Task CreateDatabaseAsync()
        {
            //DEBEMOS CREAR UN CONTENEDOR MEDIANTE SUS PROPIEDADES
            ContainerProperties properties =
                new ContainerProperties("containercoches", "/id");
            //CREAMOS LA BASE DE DATOS QUE CONTENDRA EL CONTAINER
            await this.clientCosmos.CreateDatabaseIfNotExistsAsync
                ("vehiculoscosmos");
            //DESPUES DE CREAR LA BASE DE DATOS, CREAMOS EL CONTAINER
            await this.clientCosmos.GetDatabase("vehiculoscosmos")
                .CreateContainerIfNotExistsAsync(properties);
        }

        //METODO PARA INSERTAR ITEMS
        public async Task InsertVehiculoAsync(Vehiculo car)
        {
            //EN EL MOMENTO DE INSERTAR OBJETOS DENTRO 
            //DE COSMOS, DEBEMOS INDICAR EL OBJETO Y SU 
            //PARTITION KEY DE FORMA EXPLICITA
            await this.containerCosmos.CreateItemAsync<Vehiculo>
                (car, new PartitionKey(car.Id));
        }

        //METODO PARA RECUPERAR TODOS LOS VEHICULOS
        public async Task<List<Vehiculo>> GetVehiculosAsync()
        {
            //LOS DATOS SE RECUPERAN MEDIANTE Iterator, 
            //ES DECIR, UN BUCLE QUE FUNCIONA MIENTRAS QUE 
            //EXISTAN REGISTROS
            var query =
                this.containerCosmos.GetItemQueryIterator<Vehiculo>();
            List<Vehiculo> coches = new List<Vehiculo>();
            while (query.HasMoreResults)
            {
                var results = await query.ReadNextAsync();
                //DENTRO DE RESULTS TENDREMOS MULTIPLES DATOS
                coches.AddRange(results);
            }
            return coches;
        }

        //METODO PARA MODIFICAR UN VEHICULO
        public async Task UpdateVehiculoAsync(Vehiculo car)
        {
            //VAMOS A UTILIZAR UN METODO LLAMADO Upsert
            //QUE PERMITE MODIFICAR EL ITEM
            //SI ENCUENTRA EL ITEM, LO MODIFICA
            //SI NO ENCUENTRA EL ITEM, LO INSERTA
            await this.containerCosmos.UpsertItemAsync<Vehiculo>
                (car, new PartitionKey(car.Id));
        }

        //METODO PARA ELIMINAR UN VEHICULO
        public async Task DeleteVehiculoAsync(string id)
        {
            //PARA ELIMINAR NECESITAMOS EL ID DEL OBJETO Y 
            //TAMBIEN SU PARTITION KEY
            await this.containerCosmos.DeleteItemAsync<Vehiculo>
                (id, new PartitionKey(id));
        }

        //METODO PARA BUSCAR UN VEHICULO POR SU ID
        public async Task<Vehiculo> FindVehiculoAsync(string id)
        {
            ItemResponse<Vehiculo> response = await
                this.containerCosmos.ReadItemAsync<Vehiculo>
                (id, new PartitionKey(id));
            return response.Resource;
        }

        public async Task<List<Vehiculo>> GetVehiculosMarcaAsync(string marca)
        {
            // LOS FILTROS SE CONCATENAN
            string sql = "select * from c where c.Marca='" + marca + "'";
            // PARA FILTRAR SE UTILIZA UNA CLASE LLAMADA QUERYDEFINITION
            // PARA APLICAR LOS FILTROS
            QueryDefinition definition = new QueryDefinition(sql);
            var query = this.containerCosmos
                .GetItemQueryIterator<Vehiculo>(definition);
            List<Vehiculo> cars = new List<Vehiculo>();
            while (query.HasMoreResults)
            {
                var results = await query.ReadNextAsync();
                cars.AddRange(results);
            }
            return cars;
        }
    }
}
