using System;
using System.Globalization;
using System.IO;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.WebsitePrototype.Configuration;

namespace Sitecore.WebsitePrototype.Pipelines.ItemResolver
{
    public class ResolveHtmlFile : ItemResolverProcessorBase
    {
        public override void Process(ItemResolverArgs args)
        {
            args.FilePath = GetFilePath(args.OriginalItem.Paths.FullPath, args);

            if (!string.IsNullOrEmpty(args.FilePath))
            {
                args.AbortPipeline();
            }
        }

        private static string GetFilePath(string itemPath, ItemResolverArgs args)
        {
            itemPath = itemPath.Replace("/sitecore/layout/", string.Empty);
            var index = itemPath.IndexOf('/');
            var section = itemPath.Substring(0, index);

            var relativePath = itemPath.Replace(section + "/", "/Prototype/" + section + "/") + ".html";

            var filePath = HttpContext.Current.Server.MapPath(relativePath);

            return File.Exists(filePath) ? filePath : string.Empty;
        }
    }
}