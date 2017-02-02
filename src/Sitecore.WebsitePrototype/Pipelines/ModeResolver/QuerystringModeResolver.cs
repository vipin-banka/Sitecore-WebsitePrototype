using System;
using System.Web;

namespace Sitecore.WebsitePrototype.Pipelines.ModeResolver
{
    public class QuerystringModeResolver : ModeResolverProcessorBase
    {
        public override void Process(ModeResolverArgs args)
        {
            string mode;
            if (args.IsPrototypeMode)
            {
                mode = HttpContext.Current.Request["r_mode"];
                args.IsPrototypeMode = string.IsNullOrEmpty(mode) || !mode.Equals("no", StringComparison.OrdinalIgnoreCase);
                return;
            }

            mode = HttpContext.Current.Request["r_mode"];
            args.IsPrototypeMode = !string.IsNullOrEmpty(mode) && mode.Equals("prototype", StringComparison.OrdinalIgnoreCase);
        }
    }
}
