using System;

namespace LexicalAnalyser.IO
{
    public interface IStringSplitter
    {
        string[] SplitContent(string content);
    }

    public class StringSplitter : IStringSplitter
    {
        // Use 'new' to initialize the array of separators
        private static readonly char[] Separators = new char[] { ' ', '\n' };

        public string[] SplitContent(string content)
        {
            // Use 'new string[0]' instead of '[]' for returning an empty array
            if (string.IsNullOrWhiteSpace(content))
            {
                return new string[0];
            }

            var splittedContent = content.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            return splittedContent;
        }
    }
}
