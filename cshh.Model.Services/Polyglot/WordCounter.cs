using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Model.Services.Polyglot
{
    //todo move to model

    public class WordCounter
    {
        public string Text { get; set; }
        public WordInfo[] Words { get; set; }
        public List<string> Separators { get; }
        public static string[] DefaultSeparators { get; }
            = new[] { " ", ";", ",", "\"", ".", "“", "?", "!", "\r", "\n", "\t" };

        public WordCounter(string text, string[] separators = null)
        {
            Text = text;

            if(separators != null && separators.Count() > 0)
                Separators = separators.ToList();
            else
                Separators = DefaultSeparators.ToList();
        }

        public WordInfo[] GetWordsInfo()
        {
            var parseWords = ParseWords(Text, Separators.ToArray());
            var distinctWords = GetDistinctWords(parseWords);


            Words = distinctWords.OfType<DictionaryEntry>()
                .Select(de => new WordInfo() { Word = de.Key.ToString(), Count = (int)de.Value })
                .ToArray();

            return Words;
        }

        public static string[] ParseWords(string text, string[] separators)
        {
            var split = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return split.Select(w => w.Trim().ToLower()).ToArray();
        }

        public static Hashtable GetDistinctWords(string[] words)
        {
            int count;
            int firstCount = 1;
            Hashtable hashtable = new Hashtable();

            foreach(var word in words)
            {
                if(hashtable.ContainsKey(word))
                {
                    count = (int)hashtable[word];
                    hashtable[word] = ++count;
                }
                else
                    hashtable.Add(word, firstCount);
            }

            return hashtable;
        }
    }

    public class WordInfo
    {
        public string Word { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"{Word} {Count}";
        }
    }
}
