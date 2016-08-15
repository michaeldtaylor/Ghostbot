using System;
using System.IO;
using Ghostbot.Domain;
using SQLite;

namespace Ghostbot.Infrastructure
{
    public static class SQLiteHelper
    {
        public const string DatabaseFileName = "ghostbot.db";
        public static readonly string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseFileName);

        public static void CreateDatabase()
        {
            WithConnection(c => c.CreateTable<DiscordUser>());
        }

        public static TResult WithConnection<TResult>(Func<SQLiteConnection, TResult> func)
        {
            using (var connection = new SQLiteConnection(DatabasePath))
            {
                return func(connection);
            }
        }
    }
}