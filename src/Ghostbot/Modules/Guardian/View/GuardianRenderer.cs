using System.Linq;
using System.Text;
using Destiny.Net.Core.Model;
using Destiny.Net.Core.Model.Responses;

namespace Ghostbot.Modules.Guardian.View
{
    public static class GuardianRenderer
    {
        public static string Render(BungieAccountResponse bungieAccount, Platform platform)
        {
            var destinyAccount = bungieAccount.DestinyAccounts.First();
            var builder = new StringBuilder();

            builder.AppendLine($"Name:       {destinyAccount.UserInfo.DisplayName}");
            builder.AppendLine($"Platform:   {platform}");
            builder.AppendLine($"Grimoire:   {destinyAccount.GrimoireScore}");
            builder.AppendLine();

            foreach (var character in destinyAccount.Characters)
            {
                builder.AppendLine($"{character.CharacterClass.ClassName.PadRight(20)}{character.Level}");
                builder.AppendLine($"{character.Race.RaceName} {character.Gender.GenderName}".PadRight(18) + $"+{character.PowerLevel}");
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}