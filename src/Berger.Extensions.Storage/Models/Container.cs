using Berger.Extensions.Abstractions;

namespace Berger.Extensions.Storage
{
    public class Container : BaseEntity, IContainer
    {
        public string Url { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Visibility { get; set; } = true;
    }
}