namespace Ghostbot.Console
{
    public class Program
    {
        static void Main(string[] args) => Start();

        static void Start()
        {
            var ghostbotClient = new GhostbotClient();

            ghostbotClient.Start();
        }
    }
}
