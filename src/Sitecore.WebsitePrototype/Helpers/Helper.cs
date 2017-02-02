using System;
using System.IO;
using System.Web;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.WebsitePrototype.Pipelines.ItemResolver;
using Sitecore.WebsitePrototype.Pipelines.ModeResolver;

namespace Sitecore.WebsitePrototype.Helpers
{
    public static class Helper
    {
        public static bool IsPrototypeMode()
        {
            var args = new ModeResolverArgs();
            ModeResolverPipeline.Run(args);
            return args.IsPrototypeMode;
        }

        public static ItemResolverArgs GetPrototypeItem(Guid itemId)
        {
            return GetPrototypeItem(new ID(itemId));
        }

        public static ItemResolverArgs GetPrototypeItem(ID itemId)
        {
            var item = Context.Database.GetItem(itemId);
            if (item != null)
            {
                return GetPrototypeItem(item);
            }

            return null;
        }
        public static ItemResolverArgs GetPrototypeItem(Item item)
        {
            var args = new ItemResolverArgs();
            args.OriginalItem = item;
            ItemResolverPipeline.Run(args);
            return args;
        }
    }
}