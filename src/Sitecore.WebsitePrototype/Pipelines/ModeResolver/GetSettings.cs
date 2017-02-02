using Sitecore.WebsitePrototype.Configuration;

namespace Sitecore.WebsitePrototype.Pipelines.ModeResolver
{
    public class GetSettings : ModeResolverProcessorBase
    {
        public override void Process(ModeResolverArgs args)
        {
            args.Settings = SettingsLoader.GetSettings();
        }
    }
}