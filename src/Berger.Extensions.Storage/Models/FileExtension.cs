using Berger.Extensions.Abstractions;

namespace Berger.Extensions.Storage
{
    public class FileExtension : IFileExtension
    {
        public FileExtensionType ExtensionType { get; private set; }
        public FileExtension(FileExtensionType type)
        {
            ExtensionType = type;
        }
        public override string ToString()
        {
            return ExtensionType.ToString().ToLower();
        }
    }
}