using System.Linq;
using System.Collections.Generic;
using Common.Utility;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNumeric(this string str)
        {
            return !string.IsNullOrEmpty(str) && str.All(char.IsDigit);
        }
    
        public static bool IsIdentifier(this string str)
        {
            return !string.IsNullOrEmpty(str) && str[0].IsLetter() && str.All(x => x.IsDigitOrLetter());
        }

        public static bool IsKeyword(this string str)
        {
            return Constants.Keywords.Contains(str);
        }
    
        public static bool IsDigit(this char ch) =>  char.IsDigit(ch);
    
        public static bool IsLetter(this char ch) => char.IsLetter(ch);

        public static bool IsDigitOrLetter(this char ch) => ch.IsDigit() || ch.IsLetter();

        public static bool HaveCommonStartWithAny(this char ch, IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                if (key.StartsWith(ch))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
