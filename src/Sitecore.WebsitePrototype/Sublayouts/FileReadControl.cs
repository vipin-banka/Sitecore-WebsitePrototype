using System;
using System.IO;

namespace Sitecore.WebsitePrototype.Sublayouts
{
    public class FileReadControl : BaseSublayout
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var filePath = DataSource;
            if (!string.IsNullOrEmpty(filePath))
            {
                Response.Write(File.ReadAllText(filePath));
            }
        }
    }
}
