using Sitecore.Diagnostics;
using Sitecore.Pipelines;

namespace Sitecore.WebsitePrototype.Pipelines.ModeResolver
{
    public class ModeResolverPipeline
    {
        public static void Run(ModeResolverArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            CorePipeline.Run("websiteprototype.moderesolver", args);
        }
    }
}
