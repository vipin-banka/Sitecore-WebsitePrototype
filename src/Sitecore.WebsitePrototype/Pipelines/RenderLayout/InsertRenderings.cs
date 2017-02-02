using System.Linq;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.RenderLayout;
using Sitecore.WebsitePrototype.Helpers;

namespace Sitecore.WebsitePrototype.Pipelines.RenderLayout
{
    public class InsertRenderings : RenderLayoutProcessor
    {
        public override void Process(RenderLayoutArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            if (Context.Item == null || Context.Page == null || Context.Page.Renderings == null || Context.Page.Renderings.Count <= 0)
                return;

            if (!Helper.IsPrototypeMode())
                return;

            using (new ProfileSection("Insert prototype renderings into page."))
            {
                foreach (var rendering in Context.Page.Renderings.ToList())
                {
                    var itemResolverInfo = Helper.GetPrototypeItem(rendering.RenderingID);

                    if (itemResolverInfo != null)
                    {
                        if (itemResolverInfo.PrototypeItem != null
                            && itemResolverInfo.PrototypeItem.ID != rendering.RenderingID)
                        {
                            rendering.SetRendering(itemResolverInfo.PrototypeItem.ID);
                        }
                        else if (!string.IsNullOrEmpty(itemResolverInfo.FilePath))
                        {
                            rendering.SetRendering(new ID(Constants.Renderings.FileReadId));
                            rendering.Settings.DataSource = itemResolverInfo.FilePath;
                        }
                    }
                }
            }
        }
    }
}
