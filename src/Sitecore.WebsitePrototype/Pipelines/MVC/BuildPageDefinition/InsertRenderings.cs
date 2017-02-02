using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Pipelines.Response.BuildPageDefinition;
using Sitecore.Mvc.Presentation;
using Sitecore.WebsitePrototype.Helpers;
using Sitecore.WebsitePrototype.Renderer;

namespace Sitecore.WebsitePrototype.Pipelines.MVC.BuildPageDefinition
{
    public class InsertRenderings : BuildPageDefinitionProcessor
    {
        public override void Process(BuildPageDefinitionArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            PageDefinition result = args.Result;

            if (result == null || result.Renderings == null || result.Renderings.Count <= 0)
                return;

            if (Helper.IsPrototypeMode())
            {
                var layouts =
                    result.Renderings.Where(r => r.RenderingType.Equals("Layout", StringComparison.OrdinalIgnoreCase))
                        .ToList();

                var layoutMap = new Dictionary<Guid, ID>();

                if (layouts.Count > 0)
                {
                    foreach (var layout in layouts)
                    {
                        var itemResolverInfo = Helper.GetPrototypeItem(layout.LayoutId);
                        if (itemResolverInfo != null)
                        {
                            if (itemResolverInfo.PrototypeItem != null
                                && itemResolverInfo.PrototypeItem.ID.Guid != layout.LayoutId)
                            {
                                layoutMap.Add(layout.LayoutId, itemResolverInfo.PrototypeItem.ID);
                                layout.LayoutId = itemResolverInfo.PrototypeItem.ID.Guid;
                            }
                            else if (!string.IsNullOrEmpty(itemResolverInfo.FilePath))
                            {
                                layout.Renderer = new ReadFileRenderer { FilePath = itemResolverInfo.FilePath };
                            }
                        }
                    }
                }

                var renderings =
                    result.Renderings.Where(r => r.RenderingType.Equals("r", StringComparison.OrdinalIgnoreCase))
                        .ToList();

                if (renderings.Count > 0)
                {
                    foreach (var rendering in renderings)
                    {
                        var itemResolverInfo = Helper.GetPrototypeItem(rendering.RenderingItem.InnerItem);
                        if (itemResolverInfo != null)
                        {
                            if (itemResolverInfo.PrototypeItem != null
                                &&
                                !itemResolverInfo.PrototypeItem.ID.Guid.ToString("B")
                                    .Equals(rendering.RenderingItemPath, StringComparison.OrdinalIgnoreCase))
                            {
                                rendering.RenderingItemPath = itemResolverInfo.PrototypeItem.ID.Guid.ToString("B");
                                rendering.RenderingItem = new RenderingItem(itemResolverInfo.PrototypeItem);

                                if (layoutMap.ContainsKey(rendering.LayoutId))
                                {
                                    rendering.LayoutId = layoutMap[rendering.LayoutId].Guid;
                                }

                                rendering.Renderer = null;
                            }
                            else if (!string.IsNullOrEmpty(itemResolverInfo.FilePath))
                            {
                                rendering.Renderer = new ReadFileRenderer { FilePath = itemResolverInfo.FilePath };
                            }
                        }
                    }
                }
            }
        }
    }
}