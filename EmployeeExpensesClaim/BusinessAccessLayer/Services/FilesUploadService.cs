using AutoMapper;
using Azure.Storage.Blobs;
using EmployeeExpensesClaim.BusinessAccessLayer.Interfaces;
using EmployeeExpensesClaim.DataAccessLayer.Entities;
using EmployeeExpensesClaim.DataAccessLayer.Repositories.Interfaces;
using EmployeeExpensesClaim.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
namespace EmployeeExpensesClaim.BusinessAccessLayer.Services
{
    public class FilesUploadService : IFilesUploadService
    {
        private readonly string _connectionString;
        private readonly string _containerName;


        public FilesUploadService(IConfiguration configuration)
        {
 
            _connectionString = configuration["AzureBlobSettings:ConnectionString"];
            _containerName = configuration["AzureBlobSettings:ContainerName"];
        }
        public async Task<List<string>> UploadFilesAsync(IList<IFormFile> files)
        {
            var urls = new List<string>();

            var container = new BlobContainerClient(_connectionString, _containerName);
            await container.CreateIfNotExistsAsync();

            foreach (var file in files)
            {
                var blobClient = container.GetBlobClient(Guid.NewGuid() + "_" + file.FileName);
                using var stream = file.OpenReadStream();
                await blobClient.UploadAsync(stream, overwrite: true);
                urls.Add(blobClient.Uri.ToString());
            }

            return urls;
        }
    }
}
