using Cambridge_Crawler.Constants;
using Cambridge_Crawler.Extensions;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cambridge_Crawler.Services
{
    public class DictionaryService
    {
        public HtmlWeb Web { get; set; }
        public DictionaryService(HtmlWeb htmlWeb)
        {
            Web = htmlWeb;
        }
        public bool LookUp(string url, string word)
        {
            var htmlDoc = Web.Load(url + word);
            var wordEntryBodyNodes = htmlDoc.DocumentNode.SelectNodes(Xpaths.wordEntryBodyNodes);

            if (wordEntryBodyNodes == null)
            {
                return false;
            }
            else
            {
                foreach (var wordEntryBodyNode in wordEntryBodyNodes)
                {
                    var wordNodeXPath = wordEntryBodyNode.XPath;
                    var wordHeadXpath = $"{wordNodeXPath}/{Xpaths.wordHeadNodes}";
                    var wordTypeXpath = $"{wordNodeXPath}/{Xpaths.wordTypeNodes}";
                    var wordSenseBlockXpath = $"{wordNodeXPath}/{Xpaths.wordSenseBlockNodes}";
                    var wordPronounceXpath = $"{wordNodeXPath}/{Xpaths.wordPronounceNodes}";
                    var wordHead = htmlDoc.GetInnerTextByXpath(wordHeadXpath);
                    var wordType = htmlDoc.GetInnerTextByXpath(wordTypeXpath);
                    var wordPronounce = htmlDoc.GetInnerTextByXpath(wordPronounceXpath);
                    Console.WriteLine($"{wordHead.ToUpper()}  -  {wordType}");
                    Console.WriteLine($"{wordPronounce}");
                    var senseBlockNodes = htmlDoc.DocumentNode.SelectNodes(wordSenseBlockXpath);
                    foreach (var senseBlock in senseBlockNodes)
                    {
                        var senseBlockXpath = senseBlock.XPath;
                        var senseBodyDefinitionBlockXpath = $"{senseBlockXpath}/{Xpaths.wordSenseBodyDefBlockNodes}";
                        var senseBodyDefinitionBlocks = htmlDoc.DocumentNode.SelectNodes(senseBodyDefinitionBlockXpath);
                        if (senseBodyDefinitionBlocks != null)
                        {
                            foreach (var senseBody in senseBodyDefinitionBlocks)
                            {
                                var senseBodyXPath = senseBody.XPath;
                                var senseBodyDefinitionHeaderXpath = $"{senseBodyXPath}/{Xpaths.wordSenseBodyDefHeadNodes}";
                                var senseBodyDefinitionExampleXpath = $"{senseBodyXPath}/{Xpaths.wordSenseBodyDefExampleNodes}";
                                var def = htmlDoc.GetInnerTextByXpath(senseBodyDefinitionHeaderXpath);
                                Console.WriteLine($"Definition: {def}");
                                var exampleNodes = htmlDoc.DocumentNode.SelectNodes(senseBodyDefinitionExampleXpath);
                                if (exampleNodes != null)
                                {
                                    foreach (var example in exampleNodes)
                                    {
                                        Console.WriteLine(example.InnerText);
                                    }
                                }
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
            }
            return true;
        }
    }
}
