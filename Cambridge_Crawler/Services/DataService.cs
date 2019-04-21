using Cambridge_Crawler.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cambridge_Crawler.Services
{
    public static class DataService
    {

        //public DataService()
        //{
        //}

        public static void SaveWordList(IEnumerable<Word> wordList)
        {
            var wordListJsonString = JsonConvert.SerializeObject(wordList);
            if (!File.Exists("dict_a.txt"))
            {
                File.Create("dict_a.txt");
            }

            try
            {
                File.AppendAllText("dict_a.txt", wordListJsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}

