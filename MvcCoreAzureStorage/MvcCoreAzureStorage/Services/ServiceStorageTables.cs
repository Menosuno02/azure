using Azure.Data.Tables;
using MvcCoreAzureStorage.Models;

namespace MvcCoreAzureStorage.Services
{
    public class ServiceStorageTables
    {
        private TableClient tableClient;

        public ServiceStorageTables(TableServiceClient tableService)
        {
            // Inyectamos el Service para crear la tabla
            // en el caso de que no exista
            this.tableClient = tableService.GetTableClient("clientes");
            // Estamos en un constructor y dicho método de objeto
            // no puede ser asíncrono
            Task.Run(async () =>
            {
                await this.tableClient.CreateIfNotExistsAsync();
            });
        }

        // Método para crear cliente
        public async Task CreateClientAsync(int id, string nombre, int salario, int edad, string empresa)
        {
            Cliente cliente = new Cliente
            {
                IdCliente = id,
                Nombre = nombre,
                Salario = salario,
                Edad = edad,
                Empresa = empresa
            };
            await this.tableClient.AddEntityAsync<Cliente>(cliente);
        }

        // Método para buscar clientes por su primary key
        // Cuando hablamos de este tipo de búsquedas dentro
        // de Azure Storage Tables, debemos buscar pos los dos
        // datos combinados, es decir, Row Key y Partition Key
        public async Task<Cliente> FindClienteAsync(string partitionKey, string rowKey)
        {
            Cliente cliente = await this.tableClient
                .GetEntityAsync<Cliente>(partitionKey, rowKey);
            return cliente;
        }

        // Método para eliminar registros
        // Para eliminar un registro único, debemos
        // enviar Partition Key y Row Key
        public async Task DeleteClientAsync(string partitionKey, string rowKey)
        {
            await this.tableClient
                .DeleteEntityAsync(partitionKey, rowKey);
        }

        // Método para recuperar todos los registros
        public async Task<List<Cliente>> GetClientesAsync()
        {
            // Para poder recuperar datos, aunque sean todos
            // es necesario indicar un query con un filter
            List<Cliente> clientes = new List<Cliente>();
            var query = this.tableClient.QueryAsync<Cliente>
                (filter: "");
            await foreach (var item in query)
            {
                clientes.Add(item);
            }
            return clientes;
        }

        public async Task<List<Cliente>> GetClientesEmpresaAsync(string empresa)
        {
            // Para filtrar, podemos utilizar la sintaxis
            // "pura" de Tables
            // string filtro = "Campo eq valor";
            // string filtro = "Cmapo lt 1000 and Campo2 gt 2500";
            // string filtroSalario = "Salario gt " + salario + " or Salario eq 200000";
            // var query = this.tableClient.QueryAsync<Cliente>(filter: filtroSalario);
            var query = this.tableClient.Query<Cliente>
                (x => x.Empresa == empresa);
            return query.ToList();
        }
    }
}
