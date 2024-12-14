using Common;

namespace Compiler.Parser.ParserStateMachine
{
    public class StateMachine : IStateMachine
    {
        private IParsingState _currentState;
        public StateMachine(IParsingResultModifier parser)
        {
            _currentState = new StartingState(parser);
        }

        public void ProcessTokens(LexicalToken token)
        {
            _currentState = _currentState.Handle(token);
        }
    }
}