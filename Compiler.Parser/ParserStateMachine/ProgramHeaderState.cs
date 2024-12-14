using Common;
using Common.Utility;
using Compiler.Parser.Exceptions;

namespace Compiler.Parser.ParserStateMachine
{
    public class ProgramHeaderState : ParsingStateBase
    {
        public ProgramHeaderState(IParsingResultModifier parser) : base(parser){ }
        public override IParsingState Handle(LexicalToken token)
        {
            if (token.IsIdentifier() && Parser.TryGetNextToken(out var newToken) &&
                newToken.IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.Semicolon]))
            {
                Parser.SetProgramName(token.Value);
                return IdentifyNextState();
            }
            throw new ParsingException("Expected program name declaration");
        }

        private IParsingState IdentifyNextState()
        {
            var nextToken = Parser.GetNextToken();
            if (nextToken.IsKeyword(Constants.TypesToLexem[LexicalTokensEnum.VarKeyword]))
            {
                return new VariableDefinitionsState(Parser);
            }
            if (nextToken.IsKeyword(Constants.TypesToLexem[LexicalTokensEnum.BeginKeyword]))
            {
                return new StatementSequenceState(Parser);
            }
            throw new ParsingException("Expected variable declarations or begin keyword.");
        }
    }
}