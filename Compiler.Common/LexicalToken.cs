using Common.Utility;

namespace Common
{
    public class LexicalToken
    {
        public LexemType Type { get; }
        public string Value { get; }

        public LexicalToken(LexemType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case LexemType.Undefined:
                    return $"{Value}:error, invalid term";
                default:
                    return $"{Constants.LexemTypeToString[Type]}:\"{Value}\"";
            }
        }

        public bool IsKeyword(string keyword)
        {
            return Type == LexemType.Keyword && Value == keyword;
        }

        public bool IsSpecialToken(string token)
        {
            return Type == LexemType.LexemSymbols && Value == token;
        }

        public bool IsIdentifier()
        {
            return Type == LexemType.Identifier;
        }

        public bool IsNumber()
        {
            return Type == LexemType.Number;
        }

        public bool IsArithmeticOperation()
        {
            return Type == LexemType.LexemSymbols &&
                   (Value == Constants.TypesToLexem[LexicalTokensEnum.Plus] ||
                    Value == Constants.TypesToLexem[LexicalTokensEnum.Minus] ||
                    Value == Constants.TypesToLexem[LexicalTokensEnum.Multiplication] ||
                    Value == Constants.TypesToLexem[LexicalTokensEnum.Division]);
        }
        
        public bool IsDataType()
        {
            return Type == LexemType.LexemSymbols &&
                   (Value == Constants.TypesToLexem[LexicalTokensEnum.String] ||
                    Value == Constants.TypesToLexem[LexicalTokensEnum.Integer]);
        }
    }

    public class IntToken : LexicalToken
    {
        public IntToken(long value) : base(LexemType.Number, value.ToString())
        {
        }
    }
}
