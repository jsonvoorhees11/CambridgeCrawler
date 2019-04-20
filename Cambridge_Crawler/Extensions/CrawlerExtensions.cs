using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cambridge_Crawler.Extensions
{
    public static class CrawlerExtensions
    {
        public static string GetInnerTextByXpath(this HtmlDocument htmlDoc, string xPath)
        {
            var node = htmlDoc.DocumentNode.SelectSingleNode(xPath);
            if (node == null)
            {
                return string.Empty;
            }
            return node.InnerText;
        }
        
        public static IEnumerable<string> GetInnerTextCollectionByXpath(this HtmlDocument htmlDoc, string xPath)
        {
            var nodes = htmlDoc.DocumentNode.SelectNodes(xPath);
            if(nodes == null)
            {
                return new string[] { };
            }
            return nodes.Select(n => n.InnerText);
        }
    }
}
