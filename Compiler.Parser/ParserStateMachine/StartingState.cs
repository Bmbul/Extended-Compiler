using Common;
using Common.Utility;
using Compiler.Parser.Exceptions;

namespace Compiler.Parser.ParserStateMachine
{
    public class StartingState : ParsingStateBase
    {
        public StartingState(IParsingResultModifier parser) : base(parser) 
        {
        }
        
        public override IParsingState Handle(LexicalToken token)
        {
            if (token.IsKeyword(Constants.TypesToLexem[LexicalTokensEnum.ProgramKeyword]))
            {
                return new ProgramHeaderState(Parser);
            }
            throw new ParsingException("Expected program start.");
        }
    }
}