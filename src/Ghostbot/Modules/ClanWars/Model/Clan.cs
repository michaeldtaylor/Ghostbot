using System;
using Ghostbot.Infrastructure;

namespace Ghostbot.Modules.ClanWars.Model
{
    public class Clan
    {
        public Clan(string title, Uri uri = null)
        {
            Title = title;
            Uri = uri;
        }

        public string Title { get; }
        public Uri Uri { get; }
        public string ClanId
        {
            get
            {
                var parts = Uri?.PathAndQuery.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                return parts == null ? string.Empty : parts[parts.Length - 1];
            }
        }

        public static Clan Parse(string clanHtmlLink)
        {
            var htmlLink = HtmlLink.Parse(clanHtmlLink);

            return htmlLink != null ? new Clan(htmlLink.Title, htmlLink.Uri) : new Clan(clanHtmlLink);
        }
    }
}