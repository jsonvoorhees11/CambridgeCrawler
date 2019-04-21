using Cambridge_Crawler.Constants;
using Cambridge_Crawler.Models;
using Cambridge_Crawler.Services;
using HtmlAgilityPack;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        const int maximumTaskCount = 500;
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
            List<Task<Word>> taskList = new List<Task<Word>>();
            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine($"Looking up for: {words[i]}");
                Console.WriteLine("----------------------------");
                var isValidWordTask = dictService.LookUp(retryPolicy, cambridgeSite,words[i]);
                taskList.Add(isValidWordTask);
                if (taskList.Count>= maximumTaskCount)
                {
                    IEnumerable<Word> wordList = GetWordListFromTask(taskList).Result;
                    Console.WriteLine($"Completed {maximumTaskCount} tasks");
                    DataService.SaveWordList(wordList.Where(w=>w!=null));
                    taskList.Clear();
                }
            }
            IEnumerable<Word> leftWordList = GetWordListFromTask(taskList).Result;
            DataService.SaveWordList(leftWordList.Where(w=>w!=null));

            //File.WriteAllLines("a_valid.txt", validWords);            

            Console.ReadKey();
        }        

        static void SetUpRetryPolicy()
        {
            retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(maxRetryAttempts, i =>
                {
                    Console.WriteLine("Retry...");
                    return TimeSpan.FromSeconds(Math.Pow(2, i));
                })
                
                ;
        }

        static async Task<IEnumerable<Word>> GetWordListFromTask(IEnumerable<Task<Word>> taskList)
        {
            IEnumerable<Word> wordList = await Task.WhenAll(taskList);
            return wordList;
        }
    }
}
