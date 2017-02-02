using System.Collections.Generic;

namespace Sitecore.WebsitePrototype.Configuration
{
    public class Settings
    {
        public IDictionary<string, string> PathMappings = new Dictionary<string, string>();

        public IDictionary<string, string> IdMappings = new Dictionary<string, string>();

        public IList<string> ExecludePaths = new List<string>();

        public IList<string> ExecludeIds = new List<string>();

        public System.DateTime? UpdateDateTime { get; set; }

        public bool EnableForAllWebsites { get; set; }

        public IList<string> EnableForWebsites = new List<string>();

        public IList<string> EnableForPaths = new List<string>();

        public IList<string> EnableForPages = new List<string>();
    }
}