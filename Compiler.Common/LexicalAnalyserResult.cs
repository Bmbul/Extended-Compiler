using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Common.Extensions;
using Common.Utility;

namespace Common
{
    public class LexicalAnalyserResult
    {
        private readonly List<LexicalToken> _parsedTokens = new List<LexicalToken>();
        private int _currentIndex = 0;

        public void AddElement(LexemType type, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));
            }

            if (value.Length > Constants.MaxLexemLength)
            {
                throw new ApplicationException($"Too long scanning. The scanning word: {value}, with the length: {value.Length}.\nThe maximum allowed length is {Constants.MaxLexemLength}.");
            }

            if (type == LexemType.Identifier && value.IsKeyword())
            {
                type = LexemType.Keyword;
            }
            _parsedTokens.Add(new LexicalToken(type, value));    
        }
        
        public override string ToString()
        {
            var resultString = new StringBuilder();

            foreach (var parsedToken in _parsedTokens)
            {
                resultString.AppendLine(parsedToken.ToString());
            }
            return resultString.ToString();
        }
        
        public bool TryGetNextToken(out LexicalToken token)
        {
            if (_currentIndex < _parsedTokens.Count)
            {
                token = _parsedTokens[_currentIndex++];
                return true;
            }
            token = null;
            return false;
        }

        public bool ContainsInvalid(out LexicalToken lexicalToken)
        {
            lexicalToken = _parsedTokens.FirstOrDefault(x => x.Type == LexemType.Undefined);
            return lexicalToken != null;
        }
    }
}