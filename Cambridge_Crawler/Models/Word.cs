using System;
using System.Collections.Generic;
using System.Text;

namespace Cambridge_Crawler.Models
{
    public class Word
    {
        public string Head { get; set; }

        public List<WordVariant> WordVariants { get; set; }

        public Word()
        {
            WordVariants = new List<WordVariant>();
        }
        
    }
}
