using System;
using System.Collections.Generic;
using Destiny.Net.Core.Model;

namespace Ghostbot.Domain
{
    public class PlatformParser : IPlatformParser
    {
        static readonly Dictionary<string, Platform> Map = new Dictionary<string, Platform>(StringComparer.CurrentCultureIgnoreCase)
        {
            { "PlayStation", Platform.PlayStation },
            { "PSN", Platform.PlayStation },
            { "Xbox", Platform.Xbox }
        };

        public Platform GetPlatform(string platformValue)
        {
            Platform platform;

            if (Map.TryGetValue(platformValue, out platform))
            {
                return platform;
            }

            throw new UnknownPlatformException(platformValue);
        }
    }
}