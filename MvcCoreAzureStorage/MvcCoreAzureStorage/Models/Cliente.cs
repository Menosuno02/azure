using Azure;
using Azure.Data.Tables;

namespace MvcCoreAzureStorage.Models
{
    public class Cliente : ITableEntity
    {
        public string Nombre { get; set; }
        public int Salario { get; set; }
        public int Edad { get; set; }

        // Necesitamos un ID del cliente
        // Debemos implementar que el ID y el row key
        // sean lo mismo
        private int _IdCliente;
        public int IdCliente
        {
            get { return this._IdCliente; }
            set
            {
                this._IdCliente = value;
                this.RowKey = value.ToString();
            }
        }

        // Quiero un campo que represente un conjunto para
        // los clientes como, por ejemplo, Empresa
        // Pondremos dicho campo como Partition Key
        public string _Empresa;
        public string Empresa
        {
            get { return this._Empresa; }
            set
            {
                this._Empresa = value;
                this.PartitionKey = value;
            }
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
