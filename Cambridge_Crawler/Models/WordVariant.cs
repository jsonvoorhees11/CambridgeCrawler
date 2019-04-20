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
        public WordVariant Type { get; set; }
        public string Pronunciation { get; set; }
        public List<WordSense> WordsSenses{ get;set; }
    }
}
