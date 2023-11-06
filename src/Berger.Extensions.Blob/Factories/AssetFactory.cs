using Berger.Extensions.Storage;
using Berger.Extensions.Abstractions;

namespace Berger.Extensions.Blob
{
    public class AssetFactory
    {
        public IAsset<T> Create<T>(T value)
        {
            return new Asset<T>(value);
        }
    }
}