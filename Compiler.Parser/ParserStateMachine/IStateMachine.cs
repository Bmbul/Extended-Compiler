using Common;

namespace Compiler.Parser.ParserStateMachine
{
    public interface IStateMachine
    {
        void ProcessTokens(LexicalToken token);
    }
}