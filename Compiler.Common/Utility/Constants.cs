using System.Collections.Generic;
namespace Common.Utility
{
    public static class Constants
    {
        public const int MaxLexemLength = 50;

        private static readonly string ProgramKeyword = "program";
        private static readonly string VarKeyword = "var";
        private static readonly string BeginKeyword = "begin";
        private static readonly string EndKeyword = "end";
        private static readonly string IntegerKeyword = "integer";
        private static readonly string StringKeyword = "string";
        
        public static readonly string WriteKeyword = "write";
        
        private static readonly string PlusToken = "+";
        private static readonly string MinusToken = "-";
        private static readonly string MultiplicationToken = "*";
        private static readonly string AssignmentToken = ":=";
        private static readonly string ColonToken = ":";
        private static readonly string SemicolonToken = ";";
        private static readonly string DotToken = ".";
        private static readonly string CommaToken = ",";
        private static readonly string OpeningBracketToken = "(";
        private static readonly string ClosingBracketToken = ")";

        public static readonly List<string> Keywords = new List<string>
        {
            ProgramKeyword,
            VarKeyword,
            BeginKeyword,
            EndKeyword,
            IntegerKeyword,
            StringKeyword,
            WriteKeyword
        };
        
        public static readonly List<string> LexicalTokens = new List<string>
        {
            PlusToken,
            MinusToken,
            MultiplicationToken,
            AssignmentToken,
            ColonToken,
            SemicolonToken,
            DotToken,
            CommaToken,
            OpeningBracketToken,
            ClosingBracketToken
        };
        
        public static readonly List<char> EndingChars = new List<char>
        {
            ' ', '\n', '\t'
        };
        
        public static readonly Dictionary<LexicalTokensEnum, string> TypesToLexem = new Dictionary<LexicalTokensEnum, string>
        {
            { LexicalTokensEnum.Plus, PlusToken },
            { LexicalTokensEnum.Minus, MinusToken},
            { LexicalTokensEnum.Multiplication, MultiplicationToken},
            { LexicalTokensEnum.Assignment, AssignmentToken },
            { LexicalTokensEnum.Colon, ColonToken },
            { LexicalTokensEnum.Semicolon, SemicolonToken },
            { LexicalTokensEnum.Dot, DotToken },
            { LexicalTokensEnum.Comma, CommaToken },
            { LexicalTokensEnum.OpeningBracket, OpeningBracketToken },
            { LexicalTokensEnum.ClosingBracket, ClosingBracketToken},
            
            { LexicalTokensEnum.ProgramKeyword, ProgramKeyword },
            { LexicalTokensEnum.VarKeyword, VarKeyword },
            { LexicalTokensEnum.BeginKeyword, BeginKeyword },
            { LexicalTokensEnum.EndKeyword, EndKeyword },
            { LexicalTokensEnum.Integer, IntegerKeyword },
            { LexicalTokensEnum.String, StringKeyword },

            { LexicalTokensEnum.WriteKeyword, WriteKeyword}
        };
        
        public static readonly Dictionary<LexemType, string> LexemTypeToString = new Dictionary<LexemType, string>
        {
            { LexemType.Undefined, "Undefined" },
            { LexemType.Number, "number" },
            { LexemType.LexemSymbols, "term" },
            { LexemType.Identifier, "ident" },
            { LexemType.Keyword, "ident" }
        };
    }
}
