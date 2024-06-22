using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Text.RegularExpressions;

namespace Berger.Extensions.Storage
{
    public class BlobService
    {
        #region Constants
        private const string pattern = @"^data:image\/[a-z]+;base64,";
        #endregion

        #region Constructors
        public BlobService()
        {
        }
        #endregion

        #region Methods
        public static async Task<string> Upload(string base64, string contentType, string container, string directory, string name)
        {
            var client = GetClient(container, directory, name);

            var data = new Regex(pattern).Replace(base64, string.Empty);

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

        public static async Task<string> Upload(Stream stream, string contentType, string container, string directory, string name)
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

        public static async Task<string> Update(string container, string path, string base64)
        {
            var client = GetClient(container, path);

            var data = new Regex(pattern).Replace(base64, string.Empty);

            var array = Convert.FromBase64String(data);

            using (var stream = new MemoryStream(array))
            {
                await client.UploadAsync(stream);
            }

            return client.Uri.AbsolutePath;
        }
        public static async Task<Response> Delete(string container, string path)
        {
            var client = GetClient(container, path);

            return await client.DeleteAsync(DeleteSnapshotsOption.IncludeSnapshots);
        }
        private static BlobClient GetClient(string container, string directory, string name)
        {
            var path = Format(directory, name);

            return BuildClient(container, path);
        }
        private static BlobClient GetClient(string container, string path)
        {
            return BuildClient(container, path);
        }
        private static BlobClient BuildClient(string container, string path)
        {
            var connection = string.Empty;

            return new BlobClient(connection, container, path);
        }
        private static string Format(string directory, string name)
        {
            return $@"{directory}/{name}";
        }
        #endregion
    }
}