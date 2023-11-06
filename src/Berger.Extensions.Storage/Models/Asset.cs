using Berger.Extensions.Abstractions;

namespace Berger.Extensions.Storage
{
    public class Asset : BaseEntityWrapper
    {
        #region Properties
        public Guid ContainerID { get; set; }
        public IModule Module { get; set; }
        public IContainer Container { get; set; }
        public string AttributeID { get; set; } = string.Empty;
        public FileExtension FileExtension { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        #endregion

        #region Methods
        public void SetName(string name)
        {
            this.Name = name;
        }
        #endregion
    }
}