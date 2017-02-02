using System.IO;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Extensions;

namespace Sitecore.WebsitePrototype.Renderer
{
    public class ReadFileRenderer : Mvc.Presentation.Renderer
    {
        public override string CacheKey
        {
            get
            {
                string filePath = FilePath;
                if (filePath.IsEmptyOrNull())
                    return null;
                return "ReadFile::" + filePath;
            }
        }

        public string FilePath { get; set; }

        public override void Render(TextWriter writer)
        {
            Assert.IsNotNull(writer, "writer");
            if (File.Exists(FilePath))
            {
                var content = File.ReadAllText(FilePath);
                if (string.IsNullOrEmpty(content))
                    return;
                writer.Write(content);
            }
        }
    }
}