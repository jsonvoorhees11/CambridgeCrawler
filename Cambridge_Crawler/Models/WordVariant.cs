using System;
using System.Collections.Generic;
using System.Text;

namespace Cambridge_Crawler.Models
{
    public enum WordType
    {
        noun,
        verb,
        adjective,
        adverb,
    };
    public class WordVariant
    {
        public string Type { get; set; }
        public string Pronunciation { get; set; }
        public List<WordSense> WordsSenses{ get;set; }
        public WordVariant()
        {
            WordsSenses = new List<WordSense>();
        }

        //Constructor chaining
        public WordVariant(string wordType) : this()
        {
            //switch (wordType.Trim().ToLower())
            //{
            //    case "noun":
            //        Type = WordType.noun;
            //        break;
            //    case "verb":
            //        Type = WordType.verb;
            //        break;
            //    case "adjective":
            //        Type = WordType.adjective;
            //        break;
            //    default:
            //        Type = WordType.adverb;
            //        break;
            //}
            Type = wordType;
        }
    }
}
