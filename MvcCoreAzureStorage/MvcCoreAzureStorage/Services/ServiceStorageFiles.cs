using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;

namespace MvcCoreAzureStorage.Services
{
    public class ServiceStorageFiles
    {
        // Todo servicio de Storage siempre utiliza
        // un client para acceder a sus recursos
        // Dicho client necesita de unas keys
        private ShareDirectoryClient root;

        // Debemos recibir las keys desde appsettings
        public ServiceStorageFiles(IConfiguration configuration)
        {
            string keys = configuration.GetValue<string>
                ("AzureKeys:StorageAccount");
            // Cada cliente Storage accede a un Share mediante las claves
            ShareClient client =
                new ShareClient(keys, "ejemplofiles");
            this.root = client.GetRootDirectoryClient();
        }

        // Método para recuperar todos los ficheros de
        // la raíz del Shared
        public async Task<List<string>> GetFilesAsync()
        {
            List<string> files = new List<string>();
            await foreach (ShareFileItem item in
                this.root.GetFilesAndDirectoriesAsync())
            {
                files.Add(item.Name);
            }

            return files;
        }

        public async Task<string> ReadFileAsync(string fileName)
        {
            // Necesitamos un client del recurso que queremos
            // recuperar (File)
            ShareFileClient file =
                this.root.GetFileClient(fileName);
            ShareFileDownloadInfo data =
                await file.DownloadAsync();
            Stream stream = data.Content;
            string contenido = "";
            using (StreamReader reader = new StreamReader(stream))
            {
                contenido = await reader.ReadToEndAsync();
            }
            return contenido;
        }

        public async Task UploadFileAsync(Stream stream, string fileName)
        {
            ShareFileClient file = this.root.GetFileClient(fileName);
            await file.CreateAsync(stream.Length);
            await file.UploadAsync(stream);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            ShareFileClient file = this.root.GetFileClient(fileName);
            await file.DeleteAsync();
        }
    }
}
