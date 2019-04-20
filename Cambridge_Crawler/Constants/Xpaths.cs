using System;
using System.Collections.Generic;
using System.Text;

namespace Cambridge_Crawler.Constants
{
    public static class Xpaths
    {
        public const string wordBodyNodes = "//*[@id=\"dataset-cald4\"]/div/div/div[1]/div/div/div";
        public const string wordExampleNodes = "//*[@id=\"cald4-1-1-1\"]/div[1]/div/span/div/span";
        public const string wordHeadNodes = "div[1]/div[1]/h2";
        public const string wordTypeNodes = "div[1]/div/span/span";
        public const string wordPronounceNodes = "div[1]/span[@class=\"uk\"]/span[@class=\"pron\"]/span";
        public const string wordSenseBlockNodes = "div[@class=\"pos-body\"]/div[@class=\"sense-block\"]";
        public const string wordSenseBodyDefBlockNodes = "div[@class=\"sense-body\"]/div[@class=\"def-block pad-indent\"]";
        public const string wordSenseBodyDefHeadNodes= "p/b[@class=\"def\"]";
        public const string wordSenseBodyDefExampleNodes = "span[@class=\"def-body\"]/div/span[@class=\"eg\"]";


    }
}
