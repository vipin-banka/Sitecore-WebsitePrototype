
namespace Sitecore.WebsitePrototype.Pipelines.ItemResolver
{
    public class VerifyExcludeId : ItemResolverProcessorBase
    {
        public override void Process(ItemResolverArgs args)
        {
            var excluded = args.Settings.ExecludeIds.Contains(args.OriginalItem.ID.ToString());

            if (excluded)
            {
                args.AbortPipeline();
            }
        }
    }
}