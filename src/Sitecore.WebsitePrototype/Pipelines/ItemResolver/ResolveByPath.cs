using System;
using System.Globalization;
using Sitecore.Data.Items;
using Sitecore.WebsitePrototype.Configuration;

namespace Sitecore.WebsitePrototype.Pipelines.ItemResolver
{
    public class ResolveByPath : ItemResolverProcessorBase
    {
        public override void Process(ItemResolverArgs args)
        {
            args.PrototypeItem = GetPrototypeItem(args.OriginalItem.Paths.FullPath, args);

            if (args.PrototypeItem != null)
            {
                args.AbortPipeline();
            }
        }

        private static Item GetPrototypeItem(string itemPath, ItemResolverArgs args)
        {
            var prototypePath = GetPath(itemPath, args.Settings);

            if (!prototypePath.Equals(itemPath, StringComparison.OrdinalIgnoreCase))
            {
                var newItem = Context.Database.GetItem(prototypePath);
                if (newItem != null)
                {
                    return newItem;
                }
            }

            return null;
        }

        private static string GetPath(string path, Settings settings)
        {
            path = path.ToLower(CultureInfo.InvariantCulture);
            foreach (var key in settings.PathMappings.Keys)
            {
                if (path.StartsWith(key, StringComparison.OrdinalIgnoreCase))
                {
                    return path.Replace(key.ToLower(CultureInfo.CurrentCulture), settings.PathMappings[key]);
                }
            }

            return path;
        }
    }
}