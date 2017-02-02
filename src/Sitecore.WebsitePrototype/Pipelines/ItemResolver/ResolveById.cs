using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.WebsitePrototype.Pipelines.ItemResolver
{
    public class ResolveById : ItemResolverProcessorBase
    {
        public override void Process(ItemResolverArgs args)
        {
            args.PrototypeItem = GetPrototypeItem(args.OriginalItem.ID, args);
            if (args.PrototypeItem != null)
            {
                args.AbortPipeline();
            }
        }

        private static Item GetPrototypeItem(ID itemId, ItemResolverArgs args)
        {
            if (args.Settings.IdMappings.ContainsKey(itemId.ToString()))
            {
                itemId = new ID(args.Settings.IdMappings[itemId.ToString()]);

                var newItem = Context.Database.GetItem(itemId);
                if (newItem != null)
                {
                    return newItem;
                }
            }

            return null;
        }
    }
}