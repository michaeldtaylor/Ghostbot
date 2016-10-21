using System;

namespace Ghostbot.Domain
{
    public class UnknownPlatformException : Exception
    {
        public UnknownPlatformException(string platformValue) :
            base($"The platform '{platformValue}' is unknown.")
        {
            PlatformValue = platformValue;
        }

        public string PlatformValue { get; }
    }
}