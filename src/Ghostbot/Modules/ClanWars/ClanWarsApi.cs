using System;

namespace Ghostbot.Modules.ClanWars
{
    public static class ClanWarsApi
    {
        public static readonly Uri BaseUri = new Uri("http://destinyclanwars.com");

        public static string GetChallengeStatusRelativeUri(int challengeId)
        {
            return $"/challenges/view/{challengeId}";
        }

        public static string GetEventRelativeUri(int eventId)
        {
            return $"/clans/event/{eventId}";
        }

        public static string GetClanEventStatusRelativeUri(int eventId, int clanId)
        {
            return $"/clans/event/{eventId}/{clanId}";
        }

        public static string GetClanRelativeUri(int clanId)
        {
            return $"/clans/view/{clanId}";
        }
    }
}