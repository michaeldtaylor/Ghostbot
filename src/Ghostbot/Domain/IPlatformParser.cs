using Destiny.Net.Core.Model;

namespace Ghostbot.Domain
{
    public interface IPlatformParser
    {
        Platform GetPlatform(string platformValue);
    }
}