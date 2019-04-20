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
        public WordType Type { get; set; }
        public string Pronunciation { get; set; }
        public string Meaning { get; set; }
        public List<string> Examples { get; set; }
    }
}
