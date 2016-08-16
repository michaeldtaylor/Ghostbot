using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using HtmlAgilityPack;

namespace Ghostbot.Infrastructure
{
    public class HtmlTableParser
    {
        public static IEnumerable<T> ParseTableRows<T>(HtmlNode contentNode) where T : class
        {
            var builder = new StringBuilder();
            var tableNode = contentNode.SelectSingleNode("//table");
            var tableBodyRowNodes = tableNode.SelectNodes("tr|tbody/tr");

            foreach (var rowNode in tableBodyRowNodes)
            {
                var cells = rowNode.SelectNodes("td");
                var cellText = cells.Select(c => c.InnerText);
                var csvLine = string.Join(",", cellText);

                builder.AppendLine(csvLine);
            }

            var engine = new FileHelperEngine<T>();

            return engine.ReadString(builder.ToString());
        }
    }
}