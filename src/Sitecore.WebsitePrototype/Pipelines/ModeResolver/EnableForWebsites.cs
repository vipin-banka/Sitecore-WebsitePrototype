namespace Sitecore.WebsitePrototype.Pipelines.ModeResolver
{
    public class EnableForWebsites : ModeResolverProcessorBase
    {
        public override void Process(ModeResolverArgs args)
        {
            if (args.IsPrototypeMode)
            {
                return;
            }

            if (Context.Site == null || Context.Database == null)
            {
                return;
            }

            var siteRootItem =
                Context.Database.GetItem(Context.Site.RootPath + Context.Site.StartPath);

            if (siteRootItem != null)
            {
                args.IsPrototypeMode = args.Settings.EnableForWebsites.Contains(siteRootItem.ID.ToString());
            }
        }
    }
}