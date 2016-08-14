using System;
using System.IO;
using Ghostbot.Domain;
using SQLite;

namespace Ghostbot.Infrastructure
{
    public class SQLiteHelper
    {
        static readonly string FilePath;

        static SQLiteHelper()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            FilePath = Path.Combine(folder, "ghostbot.db");
        }

        public static void CreateDatabase()
        {
            var connection = GetConnection();

            connection.CreateTable<DiscordUser>();
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(FilePath);
        }
    }
}