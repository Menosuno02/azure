using Azure.Data.Tables;
using Azure.Data.Tables.Sas;

namespace ApiAzureStorageTokens.Services
{
    public class ServiceSasToken
    {
        // Esta clase genera tokens para nuestra
        // tabla Alumnos
        private TableClient tableAlumnos;

        public ServiceSasToken(IConfiguration configuration)
        {
            string azureKeys =
                configuration.GetValue<string>
                ("AzureKeys:StorageAccount");
            TableServiceClient serviceClient =
                new TableServiceClient(azureKeys);
            this.tableAlumnos = serviceClient.GetTableClient("alumnos");
        }

        public string GenerateToken(string curso)
        {
            // Necesitamos el tipo de permisos de acceso
            // Por ahora, solamente vamos a dar permisos
            // de lectura
            TableSasPermissions permisos = TableSasPermissions.Read;
            // El acceso a los elementos con el token está
            // delimitado mediante un tiempo
            // Necesitamos un constructor de permisos
            // con un tiempo determinado de acceso
            TableSasBuilder builder =
                this.tableAlumnos.GetSasBuilder(permisos,
                DateTime.UtcNow.AddMinutes(30));
            // Queremos limitar el acceso por curso
            builder.PartitionKeyStart = curso;
            builder.PartitionKeyEnd = curso;
            // Con todo esto, ya podemos acceder al token de
            // acceso que será una URI con los permisos y el
            // tiempo
            Uri uriToken = this.tableAlumnos.GenerateSasUri(builder);
            // Extraemos la ruta HTTPS del Uri
            string token = uriToken.AbsoluteUri;
            return token;
        }
    }
}
