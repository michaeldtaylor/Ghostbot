namespace Ghostbot.Modules.ClanWars.Model
{
    public class ChallengeDetails
    {
        public ChallengeDetails(string issuer, string fromDate, string toDate)
        {
            Issuer = issuer;
            FromDate = fromDate;
            ToDate = toDate;
        }

        public string Issuer { get; }
        public string FromDate { get; }
        public string ToDate { get; }
    }
}