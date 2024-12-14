using Common;
using Common.Utility;
using Common.Extensions;
using System.Text;

namespace LexicalAnalyser.Analyser
{
    public class BetterLexicalAnalyser : LexicalAnalyserBase
    {
        private readonly StringBuilder currentWordBuilder = new StringBuilder();
        private LexemType currentWordType = LexemType.None;

        public BetterLexicalAnalyser(string filePath) : base(filePath)
        {
        }

        public override LexicalAnalyserResult Tokenize()
        {
            var fileContent = _fileReader.ReadFile(_filePath);
            AnalyseContent(fileContent);

            return _result;
        }

        private void AnalyseContent(string fileContent)
        {
            for (int i = 0; i < fileContent.Length; ++i)
            {
                var next = i < fileContent.Length - 1 ? fileContent[i + 1] : '\0';
                var charsToSkip = AnalyseLetter(fileContent[i], next);
                i += charsToSkip;
            }
            EndOfWord();
        }

        private int AnalyseLetter(char c, char next)
        {
            int lettersToSkip = 0;
            if (c.IsAnyOf(Constants.EndingChars))
            {
                EndOfWord();
                return lettersToSkip;
            }

            if (c.HaveCommonStartWithAny(Constants.LexicalTokens))
            {
                EndOfWord();
                currentWordBuilder.Append(c);
                if (c == ':' && next == '=')
                {
                    currentWordBuilder.Append(next);
                }
                currentWordType = LexemType.LexemSymbols;
                lettersToSkip = currentWordBuilder.Length - 1;
                EndOfWord();
                return lettersToSkip;
            }

            currentWordBuilder.Append(c);
            if (currentWordType == LexemType.Undefined)
            {
                return lettersToSkip;
            }
            if (c.IsDigit())
            {
                if (currentWordType.IsAnyOf(LexemType.None, LexemType.Number))
                {
                    currentWordType = LexemType.Number;
                }
            }
            else if (c.IsLetter())
            {
                if (currentWordType.IsAnyOf(LexemType.Number))
                {
                    currentWordType = LexemType.Undefined;
                }
                else
                {
                    currentWordType = LexemType.Identifier;
                }
            }
            else
            {
                currentWordType = LexemType.Undefined;
            }

            return lettersToSkip;
        }

        private void EndOfWord()
        {
            if (currentWordBuilder.Length <= 0)
            {
                return;
            }
            _result.AddElement(currentWordType, currentWordBuilder.ToString());
            currentWordBuilder.Clear();
            currentWordType = LexemType.None;
        }
    }
}
