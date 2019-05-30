using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using GovernmentParse.DataProviders;

namespace GovernmentParse.Helpers
{
    public static class StringHandler
    {
        public static string RemoveOddSpaces(this string item)
        {
            item = new Regex("(&nbsp|&quot|&#31|\u000A);*").Replace(item, " ");
            item = new Regex("(&#39|&cent);*").Replace(item, "'");
            item = Regex.Replace(item, @"[\u0000-\u001F]|[\xad]", string.Empty);
            return Regex.Replace(item, @"\s+", " ").Trim();
        }

        public static string CleanUrl(this string url)
        {
            return new Regex("(&amp);*").Replace(url, "&");
        }

        public static string RemoveColon(this string item)
        {
            return Regex.Replace(item, @":$", "").Trim();
        }

        public static string GetEnumMemberAttrValue(Type enumType, object enumVal)
        {
            var memInfo = enumType.GetMember(enumVal.ToString());
            var attr = memInfo[0].GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
            return attr?.Value;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static bool ContainsAny(this string source, params string[] toCheck)
        {
            foreach (string needle in toCheck)
            {
                if (source.Contains(needle, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
            }
        }

        public static bool IsNullOrEmpty(this string source, bool considerOneSpace = false)
        {
            return string.IsNullOrEmpty(source) || source.Equals(" ");
        }

        /// <summary>
        /// метод форматирует xml файл и возвращает его, преобразованного в строку
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string GetIndentXml(XmlDocument xml)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                xml.Save(writer);
            }
            return sb.ToString().Replace("utf-16", "windows-1251");
        }

        public static void AddMany(this List<string> source, params string[] valuesToAdd)
        {
            foreach (var str in valuesToAdd)
                source.Add(str);
        }

        public static string GetBitNumber(this int numb)
        {
            return (numb >= 10 ? (numb < 100 ? "00" : "0") : "000") + numb;
        }

        public static string GetDateFromVerbal(this string text)
        {
            var verbalMonth = text.Split( new []{' '}, StringSplitOptions.RemoveEmptyEntries);
            var day = Regex.IsMatch(verbalMonth[0], @"^\d{1,2}$") ? verbalMonth[0] : string.Empty;
            if (!string.IsNullOrEmpty(day) && int.Parse(day) < 10)
                day = "0" + int.Parse(day);
            var month = DictionaryInitializer.MonthsComparator.FirstOrDefault(k => verbalMonth[1].Contains(k.Key)).Value;
            var year = Regex.Match(verbalMonth[2], @"\d+").Value;
            return string.IsNullOrEmpty(day) || string.IsNullOrEmpty(month) || string.IsNullOrEmpty(year) ? text : day + "." + month + "." + year;
        }

        public static bool IsVerbalDate(this string text)
        {
            return Regex.IsMatch(text.RemoveOddSpaces().Trim(), @"^\d{1,2}\s+\w+\s+\d{4}");
        }

        public static bool IsDateOrTime(this string text)
        {
            return Regex.IsMatch(text.RemoveOddSpaces().Trim(), @"^\d+(\:|\.)\d+(\:|\.)\d+");
        }

        public static string FormatToDateTimeOrText(this string text)
        {
            return text.IsVerbalDate() ? text.GetDateFromVerbal() : text;
        }
    }
}
