using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MvcCoreAzureStorage.Models;

namespace MvcCoreAzureStorage.Services
{
    public class ServiceStorageBlobs
    {
        private BlobServiceClient client;

        public ServiceStorageBlobs(BlobServiceClient client)
        {
            this.client = client;
        }

        // Método para mostrar los containers
        public async Task<List<string>> GetContainersAsync()
        {
            List<string> containers = new List<string>();
            await foreach (BlobContainerItem item in client.GetBlobContainersAsync())
            {
                containers.Add(item.Name);
            }
            return containers;
        }

        // Método para crear un contenedor
        public async Task CreateContainerAsync(string containerName)
        {
            await client.CreateBlobContainerAsync(containerName,
                PublicAccessType.Blob);
        }

        // Método para eliminar un contenedor
        public async Task DeleteContainerAsync(string containerName)
        {
            await client.DeleteBlobContainerAsync(containerName);
        }

        // Método para mostrar los blobs de un container
        public async Task<List<BlobModel>> GetBlobsAsync(string containerName)
        {
            // Recuperamos el client del container
            BlobContainerClient containerClient =
                client.GetBlobContainerClient(containerName);
            List<BlobModel> models = new List<BlobModel>();
            await foreach (BlobItem item in containerClient.GetBlobsAsync())
            {
                // name, containerName, Url
                // Debemos crear un BlobClient si necesitamos
                // tener más información del Blob
                BlobClient blobClient =
                    containerClient.GetBlobClient(item.Name);
                BlobModel blob = new BlobModel
                {
                    Nombre = item.Name,
                    Contenedor = containerName,
                    Url = blobClient.Uri.AbsoluteUri
                };
                models.Add(blob);
            }
            return models;
        }

        // Método para eliminar un blob de un container
        public async Task DeleteBlobAsync(string containerName, string blobName)
        {
            BlobContainerClient containerClient = client.GetBlobContainerClient(containerName);
            await containerClient.DeleteBlobAsync(blobName);
        }

        // Método para subir un blob a un container
        public async Task UploadBlobAsync(string containerName, string blobName, Stream stream)
        {
            BlobContainerClient containerClient = client.GetBlobContainerClient(containerName);
            await containerClient.UploadBlobAsync(blobName, stream);
        }
    }
}
