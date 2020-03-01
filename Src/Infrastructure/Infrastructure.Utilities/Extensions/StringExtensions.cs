using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Utilities.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Arabic Ke Char \u0643 = ARABIC LETTER KAF
        /// </summary>
        public const char ArabicKeChar = (char)1603;

        /// <summary>
        /// Arabic Ye Char \u0649 = ARABIC LETTER ALEF MAKSURA
        /// </summary>
        public const char ArabicYeChar1 = (char)1609;

        /// <summary>
        /// Arabic Ye Char \u064A = ARABIC LETTER YEH
        /// </summary>
        public const char ArabicYeChar2 = (char)1610;

        /// <summary>
        /// Persian Ke Char \u06A9 = ARABIC LETTER KEHEH
        /// </summary>
        public const char PersianKeChar = (char)1705;

        /// <summary>
        /// Persian Ye Char \u06CC = 'ARABIC LETTER FARSI YEH
        /// </summary>
        public const char PersianYeChar = (char)1740;

        /// <summary>
        /// Fixes common writing mistakes caused by using a bad keyboard layout,
        /// such as replacing Arabic Ye with Persian one and so on ...
        /// </summary>
        /// <param name="data">Text to process</param>
        /// <returns>Processed Text</returns>
        public static string ApplyCorrectYeKe(this string data)
        {
            return string.IsNullOrWhiteSpace(data) ?
                string.Empty :
                data.Replace(ArabicYeChar1, PersianYeChar)
                    .Replace(ArabicYeChar2, PersianYeChar)
                    .Replace(ArabicKeChar, PersianKeChar)
                    .Trim();
        }

        public static string ConvertDigitsToLatin(this string input)
        {
            var sb = new StringBuilder();

            foreach (var c in input)
            {
                switch (c)
                {
                    case '\u06f0': //Persian digit
                    case '\u0660': //Arabic  digit
                        sb.Append('0');
                        break;
                    case '\u06f1':
                    case '\u0661':
                        sb.Append('1');
                        break;
                    case '\u06f2':
                    case '\u0662':
                        sb.Append('2');
                        break;
                    case '\u06f3':
                    case '\u0663':
                        sb.Append('3');
                        break;
                    case '\u06f4':
                    case '\u0664':
                        sb.Append('4');
                        break;
                    case '\u06f5':
                    case '\u0665':
                        sb.Append('5');
                        break;
                    case '\u06f6':
                    case '\u0666':
                        sb.Append('6');
                        break;
                    case '\u06f7':
                    case '\u0667':
                        sb.Append('7');
                        break;
                    case '\u06f8':
                    case '\u0668':
                        sb.Append('8');
                        break;
                    case '\u06f9':
                    case '\u0669':
                        sb.Append('9');
                        break;

                    default:
                        sb.Append(c);
                        break;
                }
            }

            return sb.ToString();
        }
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
        public static bool IsValidJson(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            var value = input.Trim();

            if (value.StartsWith("{") && value.EndsWith("}") || //For object
                value.StartsWith("[") && value.EndsWith("]")) //For array
            {
                try
                {
                    var obj = JToken.Parse(value);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
            }

            return false;
        }
    }
}
