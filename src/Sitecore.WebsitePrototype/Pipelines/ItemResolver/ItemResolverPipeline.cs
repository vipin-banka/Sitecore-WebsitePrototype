using Sitecore.Diagnostics;
using Sitecore.Pipelines;

namespace Sitecore.WebsitePrototype.Pipelines.ItemResolver
{
    public class ItemResolverPipeline
    {
        public static void Run(ItemResolverArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            CorePipeline.Run("websiteprototype.itemresolver", args);
        }
    }
}
