using Cambridge_Crawler.Constants;
using Cambridge_Crawler.Services;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;

namespace Cambridge_Crawler
{
    class Program
    {
        const string cambridgeSite = @"https://dictionary.cambridge.org/dictionary/english/";
        const char asciiAValue = 'a';
        const string filePath = "./350000-words.txt";
        static HtmlWeb web = new HtmlWeb();
        static DictionaryService dictService = new DictionaryService(web);
        static void Main(string[] args)
        {
            List<string> validWords = new List<string>();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var firstChars = new char[26];
            string[] words;
            for (int i = 0; i < firstChars.Length; i++)
            {
                firstChars[i] = (char)(asciiAValue + i);
            }

            try
            {
                words = File.ReadAllLines("a.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }

            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine($"Looking up for: {words[i]}");
                Console.WriteLine("----------------------------");
                var isValidWord = dictService.LookUp(cambridgeSite,words[i]);
                if (isValidWord)
                {
                    validWords.Add(words[i]);
                }
            }

            File.WriteAllLines("a_valid.txt", validWords);            

            Console.ReadKey();
        }        
    }
}
