using System.Linq;

namespace Sitecore.WebsitePrototype.Pipelines.ModeResolver
{
    public class EnableForPaths : ModeResolverProcessorBase
    {
        public override void Process(ModeResolverArgs args)
        {
            if (args.IsPrototypeMode)
            {
                return;
            }

            if (Context.Item == null)
            {
                return;
            }

            args.IsPrototypeMode = args.Settings.EnableForPaths.Any(x => Context.Item.Paths.FullPath.Contains(x));
        }
    }
}