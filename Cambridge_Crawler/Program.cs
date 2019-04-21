using Cambridge_Crawler.Constants;
using Cambridge_Crawler.Services;
using HtmlAgilityPack;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cambridge_Crawler
{
    class Program
    {
        const string cambridgeSite = @"https://dictionary.cambridge.org/dictionary/english/";
        const char asciiAValue = 'a';
        const string filePath = "./350000-words.txt";
        const int maxRetryAttempts = 5;
        static readonly TimeSpan pauseBetweenFailures = TimeSpan.FromSeconds(1);
        static AsyncRetryPolicy retryPolicy;

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
            SetUpRetryPolicy();
            List<Task<bool>> taskList = new List<Task<bool>>();
            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine($"Looking up for: {words[i]}");
                Console.WriteLine("----------------------------");
                var isValidWordTask = dictService.LookUp(retryPolicy, cambridgeSite,words[i]);
                taskList.Add(isValidWordTask);
                if (taskList.Count>200)
                {
                    Task.WaitAll(taskList.ToArray());
                    Console.WriteLine("Completed 20 tasks");
                    taskList.Clear();
                }
            }

            File.WriteAllLines("a_valid.txt", validWords);            

            Console.ReadKey();
        }        

        static void SetUpRetryPolicy()
        {
            retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);
        }
    }
}
