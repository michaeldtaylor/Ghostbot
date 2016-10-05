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

            builder.AppendLine($"Name:         {destinyAccount.UserInfo.DisplayName}");
            builder.AppendLine($"Platform:     {platform}");
            builder.AppendLine($"Characters:   {destinyAccount.Characters.Count()}");
            builder.AppendLine();

            foreach (var character in destinyAccount.Characters)
            {
                builder.AppendLine($"Class:   {character.CharacterClass.ClassName}");
                builder.AppendLine($"Gender:  {character.Gender.GenderName}");
                builder.AppendLine($"Gender:  {character.Race.RaceName}");
                builder.AppendLine($"Level:   {character.Level}");
                builder.AppendLine($"Light:   {character.PowerLevel}");
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}