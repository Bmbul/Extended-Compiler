using Common;

namespace Compiler.Parser.ParserStateMachine
{
    public abstract class ParsingStateBase : IParsingState
    {
        protected readonly IParsingResultModifier Parser;
        
        protected ParsingStateBase(IParsingResultModifier parser)
        {
            Parser = parser;
        }

        public abstract IParsingState Handle(LexicalToken token);
    }
}