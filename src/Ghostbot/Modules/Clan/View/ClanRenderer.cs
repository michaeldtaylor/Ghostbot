using System.Linq;
using System.Text;
using Destiny.Net.Core.Model;
using Destiny.Net.Core.Model.Responses;

namespace Ghostbot.Modules.Clan.View
{
    public static class ClanRenderer
    {
        public static string Render(BungieAccountResponse bungieAccount, Platform platform)
        {
            var destinyAccount = bungieAccount.DestinyAccounts.First();
            var clanId = bungieAccount.Clans.First().GroupId;
            var clan = bungieAccount.RelatedGroups[clanId];

            var builder = new StringBuilder();

            builder.AppendLine($"Name:       {destinyAccount.UserInfo.DisplayName}");
            builder.AppendLine($"Platform:   {platform}");
            builder.AppendLine($"Grimoire:   {destinyAccount.GrimoireScore}");
            builder.AppendLine($"Clan:       {clan.Name} ({clan.ClanCallSign})");
            builder.AppendLine();

            foreach (var character in destinyAccount.Characters)
            {
                var raceGender = $"{character.Race.RaceName} {character.Gender.GenderName}";
                
                builder.AppendLine($"{character.CharacterClass.ClassName.PadRight(20)}{character.Level}");
                builder.AppendLine($"{raceGender.PadRight(18)}+{character.PowerLevel}");
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}