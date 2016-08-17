using System;

namespace Ghostbot.Infrastructure
{
    public class HtmlLink
    {
        public string Title { get; set; }
        public Uri Uri { get; set; }
        
        public static HtmlLink Parse(string value)
        {
            var parts = value.Split(new [] { "[", "]", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 2)
            {
                return new HtmlLink
                {
                    Uri = new Uri(parts[1]),
                    Title = parts[0]
                };
            }

            return null;
        }

        public override string ToString()
        {
            return $"[{Title}]({Uri.AbsoluteUri})";
        }
    }
}