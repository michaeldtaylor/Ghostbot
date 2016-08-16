using System;

namespace Ghostbot.Modules.ClanWars.Model
{
    public class ChallengeStatusHeader
    {
        public ChallengeStatusHeader(string issuer, Uri eventUri, string eventTitle, string dates)
        {
            Issuer = issuer;
            EventUri = eventUri;
            EventTitle = eventTitle;
            Dates = dates;
        }

        public string Issuer { get; }
        public Uri EventUri { get; }
        public string EventTitle { get; }
        public string Dates { get; }
    }
}