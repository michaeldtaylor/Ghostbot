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

            var parts = uri?.PathAndQuery.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            if (parts?.Length > 0)
            {
                Id = int.Parse(parts[parts.Length - 1]);
            }
        }

        public Clan(HtmlLink eventLink) : this(eventLink.Title, eventLink.Uri)
        {
        }

        public int Id { get; }
        public string Title { get; }
        public Uri Uri { get; }

        public static Clan Parse(string clanHtmlLink)
        {
            var htmlLink = HtmlLink.Parse(clanHtmlLink);

            return htmlLink != null ? new Clan(htmlLink.Title, htmlLink.Uri) : new Clan(clanHtmlLink);
        }
    }
}