namespace Sitecore.WebsitePrototype.Pipelines.ModeResolver
{
    public class EnableForAllWebsites : ModeResolverProcessorBase
    {
        public override void Process(ModeResolverArgs args)
        {
            args.IsPrototypeMode = args.Settings.EnableForAllWebsites;
        }
    }
}