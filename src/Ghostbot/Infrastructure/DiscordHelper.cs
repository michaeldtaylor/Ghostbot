using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Ghostbot.Infrastructure
{
    public class DiscordHelper
    {
        public static async Task PageLinesToChannel(string[] lines, Channel channel, int pageSize = 10)
        {
            var pages = (int)Math.Ceiling(lines.Length / (decimal)pageSize);

            for (var i = 0; i < pages; i++)
            {
                var builder = new StringBuilder(pageSize);

                foreach (var line in lines.Skip(pageSize * i).Take(pageSize))
                {
                    builder.AppendLine(line);
                }

                await channel.SendMessage($"```{builder}```");
            }
        }
    }
}