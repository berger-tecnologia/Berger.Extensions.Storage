﻿using Berger.Extensions.Abstractions;

namespace Berger.Extensions.Storage
{
    public class Container : BaseEntityWrapper, IContainer
    {
        public string Url { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Visibility { get; set; } = true;
        public List<IAsset<IFileExtension>> Assets { get; set; }
    }
}