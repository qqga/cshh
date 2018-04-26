using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cshh.Asp.Models.Polyglot
{
    public class ParsedWordsViewModel
    {
        public string WordsJson { get; set; }

        public IEnumerable<SelectListItem> Languages { get; set; } = new SelectListItem[] { };
        public int Language_Id { get; set; } = 1;

        public IEnumerable<string> Sets { get; set; } = new string[] { };
        public string SetName { get; set; }

        public IEnumerable<SelectListItem> Statuses { get; set; } = new SelectListItem[] { };
        public int Status_Id { get; set; }

    }
    public class ParseWordsViewModel
    {
        public string Text { get; set; }
        public int? ForeignText_Id { get; set; }
        public bool ExcludeExists { get; set; }

        string[] _SeparatorsArr;
        public string[] SeparatorsArr
        {
            get => _SeparatorsArr; set
            {
                _SeparatorsArr = value;
                _SeparatorsStr = SeparatorViewConverter.Convert(value);
            }
        }
        string _SeparatorsStr;
        public string SeparatorsStr
        {
            get => _SeparatorsStr; set
            {
                _SeparatorsStr = value;
                _SeparatorsArr = SeparatorViewConverter.Convert(value).ToArray();
            }
        }

        public class SeparatorViewConverter
        {
            public const char SeparatorSign = '☺';
            public const char CharCodeSign = '☻';
            static string ConvertToView(string s)
            {
                return
                    s.Count() == 1 && (int)s.First() < 32 ?
                    $"{CharCodeSign}{(int)s.First()}" : s;
            }

            static string ConvertFromView(string s)
            {
                string charCodeSignStr = CharCodeSign.ToString();
                return
                s.StartsWith(charCodeSignStr) ? ((char)System.Convert.ToInt32(s.Replace(charCodeSignStr, ""))).ToString()
                : s;
            }

            public static string Convert(IEnumerable<string> s)
            {
                return SeparatorSign + string.Join($"{SeparatorSign}", s.Select(ConvertToView));
            }

            public static IEnumerable<string> Convert(string s)
            {
                return s.Split(SeparatorSign).Select(ConvertFromView);
            }
        }
    }

}