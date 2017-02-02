using Sitecore.Pipelines;
using Sitecore.WebsitePrototype.Configuration;

namespace Sitecore.WebsitePrototype.Pipelines.ModeResolver
{
    public class ModeResolverArgs : PipelineArgs
    {
        public Settings Settings { get; set; }
        public bool IsPrototypeMode { get; set; }
    }
}
