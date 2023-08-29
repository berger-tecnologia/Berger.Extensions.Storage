using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Text.RegularExpressions;

namespace Berger.Extensions.Blob
{
    public class BlobService
    {
        #region Constructors
        public BlobService()
        {
            //Configuration.ConfigureBuilder();

            //Tenant = Configuration.Get<Tenant>("Tenant");
        }
        #endregion

        #region Methods
        public async Task<string> Upload(string base64, string contentType, string container, string directory, string name)
        {
            var client = GetClient(container, directory, name);

            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64, string.Empty);

            var array = Convert.FromBase64String(data);

            var options = new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders()
                {
                    ContentType = contentType
                }
            };

            using (var stream = new MemoryStream(array))
            {
                await client.UploadAsync(stream, options);
            }

            return client.Uri.OriginalString;
        }

        public async Task<string> Upload(Stream stream, string contentType, string container, string directory, string name)
        {
            var client = GetClient(container, directory, name);

            var options = new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders()
                {
                    ContentType = contentType
                }
            };

            await client.UploadAsync(stream, options);

            return client.Uri.OriginalString;
        }

        public async Task<string> Update(string container, string path, string base64)
        {
            var client = GetClient(container, path);

            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64, string.Empty);

            var array = Convert.FromBase64String(data);

            using (var stream = new MemoryStream(array))
            {
                await client.UploadAsync(stream);
            }

            return client.Uri.AbsolutePath;
        }
        public async Task<Response> Delete(string container, string path)
        {
            var client = GetClient(container, path);

            return await client.DeleteAsync(DeleteSnapshotsOption.IncludeSnapshots);
        }
        private BlobClient GetClient(string container, string directory, string name)
        {
            var path = Format(directory, name);

            return BuildClient(container, path);
        }
        private BlobClient GetClient(string container, string path)
        {
            return BuildClient(container, path);
        }
        private BlobClient BuildClient(string container, string path)
        {
            var connection = string.Empty;

            return new BlobClient(connection, container, path);
        }
        private string Format(string directory, string name)
        {
            return $@"{directory}/{name}";
        }
        #endregion
    }
}