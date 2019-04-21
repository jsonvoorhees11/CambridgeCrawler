using Cambridge_Crawler.Constants;
using Cambridge_Crawler.Extensions;
using Cambridge_Crawler.Models;
using HtmlAgilityPack;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
        public async Task<Word> LookUp(AsyncRetryPolicy retryPolicy, string url, string word)
        {
            var wordModel = new Word();
            HtmlDocument htmlDoc = new HtmlDocument();

            await retryPolicy.ExecuteAsync(async () =>
            {
                htmlDoc = await Web.LoadFromWebAsync(url + word);
                
            });
            var wordEntryBodyNodes = htmlDoc.DocumentNode.SelectNodes(Xpaths.wordEntryBodyNodes);

            if (wordEntryBodyNodes == null)
            {
                return null ;
            }
            else
            {
                wordModel.Head = word;
                var wordVariants = new List<WordVariant>();
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

                    //wordModel.WordVariants.Add(new WordVariant(wordType) { Pronunciation = wordPronounce });
                    var wordVariant = new WordVariant(wordType);
                    wordVariant.Pronunciation = wordPronounce;
                    var senseBlockNodes = htmlDoc.DocumentNode.SelectNodes(wordSenseBlockXpath);

                    if (senseBlockNodes != null)
                    {
                        List<WordSense> wordSenses = new List<WordSense>();
                        foreach (var senseBlock in senseBlockNodes)
                        {
                            var senseBlockXpath = senseBlock.XPath;
                            var senseBodyDefinitionBlockXpath = $"{senseBlockXpath}/{Xpaths.wordSenseBodyDefBlockNodes}";
                            var senseBodyDefinitionBlocks = htmlDoc.DocumentNode.SelectNodes(senseBodyDefinitionBlockXpath);
                            if (senseBodyDefinitionBlocks != null)
                            {
                                var wordSense = new WordSense();
                                foreach (var senseBody in senseBodyDefinitionBlocks)
                                {
                                    var senseBodyXPath = senseBody.XPath;
                                    var senseBodyDefinitionHeaderXpath = $"{senseBodyXPath}/{Xpaths.wordSenseBodyDefHeadNodes}";
                                    var senseBodyDefinitionExampleXpath = $"{senseBodyXPath}/{Xpaths.wordSenseBodyDefExampleNodes}";
                                    var def = htmlDoc.GetInnerTextByXpath(senseBodyDefinitionHeaderXpath);

                                    wordSense.Definition = def;

                                    var exampleNodes = htmlDoc.DocumentNode.SelectNodes(senseBodyDefinitionExampleXpath);
                                    if (exampleNodes != null)
                                    {
                                        foreach (var example in exampleNodes)
                                        {
                                            wordSense.Examples.Add(example.InnerText);
                                        }
                                    }

                                }
                                wordSenses.Add(wordSense);
                            }
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                        wordVariant.WordsSenses = wordSenses;
                    }
                    wordVariants.Add(wordVariant);
                }
                wordModel.WordVariants = wordVariants;
            }
            return wordModel;
        }
    }
}
