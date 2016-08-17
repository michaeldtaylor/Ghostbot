using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using HtmlAgilityPack;

namespace Ghostbot.Infrastructure
{
    public class HtmlHelper
    {
        public static IEnumerable<T> ParseTableRows<T>(HtmlNode contentNode, Uri baseUri = null) where T : class
        {
            var builder = new StringBuilder();
            var tableNode = contentNode.SelectSingleNode("//table");
            var tableBodyRowNodes = tableNode.SelectNodes("tr|tbody/tr");

            foreach (var rowNode in tableBodyRowNodes)
            {
                var cellNodes = rowNode.SelectNodes("td");
                var cellText = cellNodes.Select(c =>
                {
                    HtmlLink htmlLink;

                    var result = TryParseLink(c, baseUri, out htmlLink) ? htmlLink.ToString() : c.InnerText;

                    return $"\"{result}\"";
                }).ToList();

                var csvLine = string.Join(",", cellText);

                builder.AppendLine(csvLine);
            }

            var engine = new FileHelperEngine<T>();

            return engine.ReadString(builder.ToString());
        }

        public static HtmlLink ParseLink(HtmlNode contentNode, Uri baseUri)
        {
            var anchorNode = contentNode.SelectSingleNode("a");
            var uriString = anchorNode.Attributes[0].Value;

            if (uriString.StartsWith("http"))
            {
                
            }

            return new HtmlLink
            {
                Title = anchorNode.InnerText,
                Uri = baseUri == null ? new Uri(uriString) : new Uri(baseUri, uriString)
            };
        }

        public static bool TryParseLink(HtmlNode contentNode, Uri baseUri, out HtmlLink htmlLink)
        {
            var anchorNode = contentNode.SelectSingleNode("a");

            if (anchorNode == null)
            {
                htmlLink = null;

                return false;
            }

            htmlLink = ParseLink(contentNode, baseUri);

            return true;
        }
    }
}