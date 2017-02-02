using System.Globalization;
using System.Linq;

namespace Sitecore.WebsitePrototype.Pipelines.ItemResolver
{
    public class VerifyExcludePath : ItemResolverProcessorBase
    {
        public override void Process(ItemResolverArgs args)
        {
            var excluded = args.Settings.ExecludePaths.Select(i=>i.ToLower(CultureInfo.CurrentCulture)).Contains(args.OriginalItem.Parent.Paths.FullPath.ToLower(CultureInfo.CurrentCulture));

            if (excluded)
            {
                args.AbortPipeline();
            }
        }
    }
}