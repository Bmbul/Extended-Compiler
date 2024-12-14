using Common;

namespace Compiler.Parser.ParserStateMachine
{
    public interface IParsingState
    {
        IParsingState Handle(LexicalToken token);
    }
}