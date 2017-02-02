
using Sitecore.Data.Items;
using Sitecore.Pipelines;
using Sitecore.WebsitePrototype.Configuration;

namespace Sitecore.WebsitePrototype.Pipelines.ItemResolver
{
    public class ItemResolverArgs : PipelineArgs
    {
        public Settings Settings { get; set; }

        public Item OriginalItem { get; set; }

        public Item PrototypeItem { get; set; }

        public string ComponentType { get; set; }

        public string FilePath { get; set; }
}
}
