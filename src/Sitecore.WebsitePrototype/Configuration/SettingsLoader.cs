using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Text;

namespace Sitecore.WebsitePrototype.Configuration
{
    public static class SettingsLoader
    {
        private static readonly object LockObject = new object();

        private static Settings _settings;

        public static Settings GetSettings()
        {
            if (Context.Database == null)
            {
                return new Settings();
            }

            var settingItem = Context.Database.GetItem(Constants.ItemIds.SettingsId);
            if (settingItem == null)
            {
                ////throw new InvalidOperationException("Prototype Mode settings item missing.");
                return new Settings();
            }

            if (_settings == null || !_settings.UpdateDateTime.HasValue || _settings.UpdateDateTime < settingItem.Statistics.Updated)
            {
                lock (LockObject)
                {
                    _settings = LoadSettings(settingItem);
                    _settings.UpdateDateTime = settingItem.Statistics.Updated;
                }
            }

            return _settings;
        }

        private static Settings LoadSettings(Item settingItem)
        {
            var settings = new Settings();

            var pathMappings = settingItem.Fields[Constants.FieldIds.Settings.PathMappings].Value;
            MapIncludeSettings(pathMappings, (o, p) => settings.PathMappings.Add(o.Paths.FullPath, p.Paths.FullPath));

            var idMappings = settingItem.Fields[Constants.FieldIds.Settings.IdMappings].Value;
            MapIncludeSettings(idMappings, (o, p) => settings.IdMappings.Add(o.ID.ToString(), p.ID.ToString()));

            var excludePaths = settingItem.Fields[Constants.FieldIds.Settings.ExcludePaths].Value;
            MapExcludeSettings(excludePaths, o => settings.ExecludePaths.Add(o.Paths.ContentPath));

            var excludeIds = settingItem.Fields[Constants.FieldIds.Settings.ExcludeIds].Value;
            MapExcludeSettings(excludeIds, o => settings.ExecludeIds.Add(o.ID.ToString()));

            settings.EnableForAllWebsites =
                "1".Equals(settingItem.Fields[Constants.FieldIds.Settings.EnableForAllWebsites].Value,
                    StringComparison.OrdinalIgnoreCase);

            var enableForWebsitesIds = settingItem.Fields[Constants.FieldIds.Settings.EnableForWebsites].Value;
            MapExcludeSettings(enableForWebsitesIds, o => settings.EnableForWebsites.Add(o.ID.ToString()));

            var enableForPathIds = settingItem.Fields[Constants.FieldIds.Settings.EnableForPaths].Value;
            MapExcludeSettings(enableForPathIds, o => settings.EnableForPaths.Add(o.ID.ToString()));

            var enableForPagesIds = settingItem.Fields[Constants.FieldIds.Settings.EnableForPages].Value;
            MapExcludeSettings(enableForPagesIds, o => settings.EnableForPages.Add(o.Paths.FullPath));
            
            return settings;
        }

        private static void MapIncludeSettings(string mappings, Action<Item, Item> action)
        {
            if (string.IsNullOrEmpty(mappings))
            {
                return;
            }

            var items = new ListString(mappings);

            foreach (var id in items)
            {
                var item = Context.Database.GetItem(id);
                if (item != null && item.TemplateID == new ID(Constants.TemplateIds.MappingTemplateId))
                {
                    var originalItemId = item.Fields[Constants.FieldIds.Mapping.OriginalItem].Value;
                    var prototypeItemId = item.Fields[Constants.FieldIds.Mapping.PrototypeItem].Value;

                    if (!string.IsNullOrEmpty(originalItemId)
                        && !string.IsNullOrEmpty(prototypeItemId))
                    {
                        var originalItem = Context.Database.GetItem(originalItemId);
                        if (originalItem != null)
                        {
                            var prototypeItem = Context.Database.GetItem(prototypeItemId);
                            if (prototypeItem != null)
                            {
                                action(originalItem, prototypeItem);
                            }
                        }
                    }
                }
            }
        }

        private static void MapExcludeSettings(string mappings, Action<Item> action)
        {
            if (string.IsNullOrEmpty(mappings))
            {
                return;
            }

            var items = new ListString(mappings);

            foreach (var id in items)
            {
                var item = Context.Database.GetItem(id);
                if (item != null)
                {
                    action(item);
                }
            }
        }
    }
}