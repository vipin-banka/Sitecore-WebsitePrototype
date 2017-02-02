#region

using System.Web.UI;
using Sitecore.Web.UI.WebControls;

#endregion

namespace Sitecore.WebsitePrototype.Sublayouts
{
    public class BaseSublayout : UserControl
    {
        public string DataSource
        {
            get
            {
                if (Parent is Sublayout)
                {
                    return ((Sublayout)Parent).DataSource;
                }

                return string.Empty;
            }
        }
    }
}