using System;
using System.Collections.Generic;
using System.Text;

namespace Cambridge_Crawler.Models
{
    public class WordSense
    {
        public string Definition { get; set; }
        public List<string> Examples { get; set; }
        public WordSense()
        {
            Examples = new List<string>();
        }
    }
}
