using Common;
using Common.Utility;
using Compiler.Parser.Exceptions;

namespace Compiler.Parser.ParserStateMachine
{
    public class StatementSequenceState : ParsingStateBase
    {
        public StatementSequenceState(IParsingResultModifier parser) : base(parser)
        {
        }

        public override IParsingState Handle(LexicalToken token)
        {
            Parser.AddStatementsSection();
            while (true)
            {
                if (token.IsKeyword(Constants.TypesToLexem[LexicalTokensEnum.EndKeyword]))
                {
                    if (Parser.GetNextToken().IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.Dot]))
                    {
                        Parser.GenerateExitWithLastOperationResult();
                        return null; // this is the end
                    }

                    throw new ParsingException("Ending `.` char expected");
                }

                if (Parser.IsInValidVariableToken(token)) // a
                {
                    throw new ParsingException("Use of undeclared identifier.");
                }
                ReadAssignmentOperator(); // :=
                var firstVariable = GetNextVariable(); // b

                var possibleArithmeticOperator = Parser.GetNextToken();
                if (possibleArithmeticOperator.IsArithmeticOperation()) // + or -
                {
                    var secondVariable = GetNextVariable(); // c

                    if (!Parser.GetNextToken().IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.Semicolon])) // ;
                    {
                        throw new ParsingException($"End of statement expected.");
                    }
                    Parser.GenerateAssignmentWithOperations(token, firstVariable, secondVariable, possibleArithmeticOperator);
                }
                else if (possibleArithmeticOperator.IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.Semicolon])) // ;
                {
                    Parser.GenerateSimpleAssignment(token, firstVariable);
                }
                else
                {
                    throw new ParsingException($"Expected end of statement or arithmetic operator.", possibleArithmeticOperator);
                }
                token = Parser.GetNextToken();
            }
        }

        private void ReadAssignmentOperator()
        {
            var assignmentOperator = Parser.GetNextToken();
            if (!assignmentOperator.IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.Assignment]))
            {
                throw new ParsingException($"Assignment operator expected.", assignmentOperator);
            }
        }

        private LexicalToken GetNextVariable()
        {
            var variable = Parser.GetNextToken();
            if (variable.IsIdentifier())
            {
                if (!Parser.IdentifierExists(variable.Value))
                {
                    throw new ParsingException("Use of undeclared identifier.");
                }
            }
            else if(!variable.IsNumber())
            {
                throw new ParsingException("Number or Identifier expected.", variable);
            }
            return variable;
        }
    }
}