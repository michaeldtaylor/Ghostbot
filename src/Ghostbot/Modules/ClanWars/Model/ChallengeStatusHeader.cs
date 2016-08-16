using System;

namespace Ghostbot.Modules.ClanWars.Model
{
    public class ChallengeStatusHeader
    {
        public ChallengeStatusHeader(string issuer, Uri eventUri, string eventTitle, string fromDate, string toDate)
        {
            Issuer = issuer;
            EventUri = eventUri;
            EventTitle = eventTitle;
            FromDate = fromDate;
            ToDate = toDate;
        }

        public string Issuer { get; }
        public Uri EventUri { get; }
        public string EventTitle { get; }
        public string FromDate { get; }
        public string ToDate { get; }
    }
}