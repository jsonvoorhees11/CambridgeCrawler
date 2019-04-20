using System;
using System.Collections.Generic;
using System.Text;

namespace Cambridge_Crawler.Models
{
    public class Word
    {
        public string Text { get; set; }

        public List<WordVariant> WordVariants { get; set; }
        
    }
}
