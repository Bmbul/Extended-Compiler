namespace Common.Utility
{
    public enum LexemType
    {
        None,
        Undefined,
        Identifier,
        Number,
        LexemSymbols,
        Keyword
    }

    public enum DataType
    {
        Integer,
        String
    }

    public enum LexicalTokensEnum
    {
        Unknown,            // Default value
        Plus,               // +
        Minus,              // -
        Multiplication,     // *
        Division,           // /
        Assignment,         // :=
        Colon,              // :
        Semicolon,          // ;
        Dot,                // .
        Comma,              // ,
        OpeningBracket,     // (
        ClosingBracket,     // )
        
        ProgramKeyword,     // program
        VarKeyword,         // var
        BeginKeyword,       // begin
        EndKeyword,         // end
        Integer,            // integer
        String,             // string
        
        // WriteKeyword        // write
    }
}