namespace Berger.Extensions.Storage
{
    public static class FileExtensionFactory
    {
        private static readonly Dictionary<FileExtensionType, FileExtension> ExtensionsCache= new Dictionary<FileExtensionType, FileExtension>();
        public static FileExtension GetFileExtension(FileExtensionType extensionType)
        {
            if (!ExtensionsCache.TryGetValue(extensionType, out var fileExtension))
            {
                fileExtension = new FileExtension(extensionType);

                ExtensionsCache.Add(extensionType, fileExtension);
            }
            return fileExtension;
        }
    }
}