using System;
using System.Collections.Generic;
using System.IO;

namespace Split_Files
{
    class Program
    {
        const char AsciiAValue = 'a';
        const string filePath = "./350000-words.txt";
        static void Main(string[] args)
        {
            var wordsToLookUp = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    string file="a.txt";
                    char currentChar = 'a';
                    
                    List<string> wordsToAppend = new List<string>();
                    while ((line = sr.ReadLine())!=null)
                    {                       
                        if (line[0] == currentChar)
                        {
                            wordsToAppend.Add(line);
                        }
                        else
                        {
                            file = currentChar.ToString() + ".txt";
                            if (!File.Exists(currentChar.ToString() + ".txt"))
                            {
                                File.Create(file);
                            }
                            File.WriteAllLines(file, wordsToAppend);
                            wordsToAppend = new List<string>();
                            currentChar++;
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
            foreach (var line in wordsToLookUp)
            {
                Console.Write(line + " --- ");
            }
        }
    }
}
