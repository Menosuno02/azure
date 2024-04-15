using Microsoft.AspNetCore.Mvc;
using MvcCoreAzureStorage.Models;
using MvcCoreAzureStorage.Services;

namespace MvcCoreAzureStorage.Controllers
{
    public class AzureBlobsController : Controller
    {
        private ServiceStorageBlobs service;

        public AzureBlobsController(ServiceStorageBlobs service)
        {
            this.service = service;
        }

        public async Task<IActionResult> ListContainers()
        {
            List<string> containers =
                await this.service.GetContainersAsync();
            return View(containers);
        }

        public IActionResult CreateContainer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContainer(string containerName)
        {
            await this.service.CreateContainerAsync(containerName);
            return RedirectToAction("ListContainers");
        }

        public async Task<IActionResult> DeleteContainer(string containerName)
        {
            await this.service.DeleteContainerAsync(containerName);
            return RedirectToAction("ListContainers");
        }

        public async Task<IActionResult> ListBlobs(string containername)
        {
            List<BlobModel> models = await this.service.GetBlobsAsync(containername);
            return View(models);
        }

        public async Task<IActionResult> DeleteBlob(string containername, string blobname)
        {
            await this.service.DeleteBlobAsync(containername, blobname);
            return RedirectToAction("ListBlobs", new { containername = containername });
        }

        public IActionResult UploadBlob(string containername)
        {
            ViewData["CONTAINER"] = containername;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadBlob(string containername, IFormFile file)
        {
            string blobname = file.FileName;
            using (Stream stream = file.OpenReadStream())
            {
                await this.service.UploadBlobAsync(containername, blobname, stream);
            }
            return RedirectToAction("ListBlobs", new { containername = containername });
        }
    }
}
