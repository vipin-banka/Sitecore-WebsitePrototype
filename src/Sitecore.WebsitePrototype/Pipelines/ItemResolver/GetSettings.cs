using Sitecore.WebsitePrototype.Configuration;

namespace Sitecore.WebsitePrototype.Pipelines.ItemResolver
{
    public class GetSettings : ItemResolverProcessorBase
    {
        public override void Process(ItemResolverArgs args)
        {
            args.Settings = SettingsLoader.GetSettings();
        }
    }
}