using System;
using Ghostbot.Infrastructure;

namespace Ghostbot.Modules.ClanWars.Model
{
    public class ChallengeEvent
    {
        public ChallengeEvent(HtmlLink eventLink)
        {
            Title = eventLink.Title;
            Uri = eventLink.Uri;
        }

        public string Title { get; }
        public Uri Uri { get; }

        public int EventId
        {
            get
            {
                var parts = Uri.PathAndQuery.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                return int.Parse(parts[parts.Length - 1]);
            }
        }
    }

    public class ChallengeStatusHeader
    {
        public ChallengeStatusHeader(string issuer, HtmlLink eventLink, string fromDate, string toDate)
        {
            Issuer = issuer;
            Event = new ChallengeEvent(eventLink);
            FromDate = fromDate;
            ToDate = toDate;
        }

        public string Issuer { get; }
        public ChallengeEvent Event { get; }
        public string FromDate { get; }
        public string ToDate { get; }
    }
}