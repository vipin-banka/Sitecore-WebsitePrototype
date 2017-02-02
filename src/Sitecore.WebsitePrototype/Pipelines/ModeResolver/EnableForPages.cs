namespace Sitecore.WebsitePrototype.Pipelines.ModeResolver
{
    public class EnableForPages : ModeResolverProcessorBase
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

            args.IsPrototypeMode = args.Settings.EnableForPages.Contains(Context.Item.ID.ToString());
        }
    }
}