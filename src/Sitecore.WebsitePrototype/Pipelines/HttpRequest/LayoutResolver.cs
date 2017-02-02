using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;
using System;
using Sitecore.WebsitePrototype.Helpers;

namespace Sitecore.WebsitePrototype.Pipelines.HttpRequest
{
    public class LayoutResolver : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            if (!Helper.IsPrototypeMode() || string.IsNullOrEmpty(Context.Page.FilePath)) 
                return;

            if (Context.Item == null)
            {
                return;
            }

            var layoutItem = Context.Item.Visualization.Layout;

            if (layoutItem != null && Context.Page.FilePath.Equals(layoutItem.FilePath, StringComparison.OrdinalIgnoreCase))
            {
                var itemResolverInfo = Helper.GetPrototypeItem(layoutItem.InnerItem);

                if (itemResolverInfo != null)
                {
                    if (itemResolverInfo.PrototypeItem != null && itemResolverInfo.PrototypeItem.ID != layoutItem.ID)
                    {
                        Context.Page.FilePath = new LayoutItem(itemResolverInfo.PrototypeItem).FilePath;
                    }
                    else if (!string.IsNullOrEmpty(itemResolverInfo.FilePath))
                    {
                        Context.Page.FilePath = itemResolverInfo.FilePath;
                    }
                }
            }
        }
    }
}