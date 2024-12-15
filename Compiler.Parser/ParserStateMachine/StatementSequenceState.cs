using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Utility;
using Compiler.Generator.Allocator;
using Compiler.Parser.Exceptions;

namespace Compiler.Parser.ParserStateMachine
{
    public class StatementSequenceState : ParsingStateBase
    { 
        private readonly Stack<LexicalToken> _reversedPolishNotationStack = new Stack<LexicalToken>();

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
                    throw new ParsingException($"Use of undeclared identifier: {token}");
                }
                
                ReadAssignmentOperator(); // :=
                var expressionTokens = GetExpressionTokens();

                LexicalToken assigningToken = null;
                if (expressionTokens.Count > 1)
                {
                    var resultRegister = ProcessExpression(expressionTokens);
                    assigningToken = new LexicalToken(LexemType.None, resultRegister.Name);
                }
                else
                {
                    assigningToken = expressionTokens.First();
                }
                
                Parser.GenerateSimpleAssignment(token,assigningToken);

                token = Parser.GetNextToken();
            }
        }

        private List<LexicalToken> GetExpressionTokens()
        {
            List<LexicalToken> tokens = new List<LexicalToken>();
            var token = Parser.GetNextToken();
            if (token.IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.Semicolon]) || 
                token.IsArithmeticOperation())
            {
                throw new ParsingException($"Unexpected Token. Expected identifier or a constant.", token);
            }
            
            tokens.Add(token);
            while (Parser.TryGetNextToken(out token))
            {
                if (token.IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.Semicolon]))
                {
                    return tokens;
                }
                tokens.Add(token);
            }
            throw new ParsingException($"Unexpected end of expression: Expected semicolon.", token);
        }
        
        private void ReadAssignmentOperator()
        {
            var assignmentOperator = Parser.GetNextToken();
            if (!assignmentOperator.IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.Assignment]))
            {
                throw new ParsingException($"Assignment operator expected.", assignmentOperator);
            }
        }
        
        private Register ProcessExpression(IEnumerable<LexicalToken> infixTokens)
        {
            Stack<LexicalToken> stack = new Stack<LexicalToken>();
            int parenthesisCount = 0;
            LexicalToken lastToken = null;

            foreach (var token in infixTokens)
            {
                if (token.IsIdentifier() || token.IsNumber())
                {
                    _reversedPolishNotationStack.Push(token);
                }
                else if (token.IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.OpeningBracket]))
                {
                    parenthesisCount++;
                    stack.Push(token);
                }
                else if (token.IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.ClosingBracket]))
                {
                    parenthesisCount--;
                    if (parenthesisCount < 0)
                    {
                        throw new ParsingException($"Mismatched closing parenthesis. Token: {token.Value}, expression may have too many closing brackets.");
                    }

                    while (stack.Count > 0 && !stack.Peek().IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.OpeningBracket]))
                    {
                        AddTokenToStack(stack.Pop());
                    }

                    if (stack.Count == 0 || !stack.Peek().IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.OpeningBracket]))
                    {
                        throw new ParsingException($"Opening parenthesis expected before token: {token.Value}. Unmatched closing parenthesis found.");
                    }
                    stack.Pop();
                }
                else if (token.IsArithmeticOperation())
                {
                    if (lastToken?.IsArithmeticOperation() == true || lastToken?.IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.OpeningBracket]) == true)
                    {
                        throw new ParsingException($"Invalid operator placement. Found operator: {token.Value} after token: {lastToken?.Value}. Operators cannot appear consecutively or immediately after opening brackets.");
                    }

                    while (stack.Count > 0 && stack.Peek().GetPrecedence() >= token.GetPrecedence())
                    {
                        AddTokenToStack(stack.Pop());
                    }

                    stack.Push(token);
                }
                else
                {
                    throw new ParsingException($"Unexpected token: {token.Value}. Token is not recognized as a valid operator, operand, or parenthesis.");
                }

                lastToken = token;
            }

            if (parenthesisCount != 0)
            {
                throw new ParsingException($"Mismatched parentheses. Found {parenthesisCount} more opening parentheses than closing ones.");
            }

            while (stack.Count > 0)
            {
                var top = stack.Pop();
                if (top.IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.OpeningBracket]))
                {
                    throw new ParsingException("Unmatched opening parenthesis.");
                }
                AddTokenToStack(top);
            }

            if (_reversedPolishNotationStack.Count != 1)
            {
                throw new ParsingException("Invalid expression. Too many or too few operands for the operators.");
            }

            var finalToken = _reversedPolishNotationStack.Pop();
            return new Register(finalToken.Value);
        }

        private void AddTokenToStack(LexicalToken token)
        {
            if (!token.IsArithmeticOperation())
            {
                _reversedPolishNotationStack.Push(token);
                return;
            }

            if (_reversedPolishNotationStack.Count < 2)
            {
                throw new ParsingException("Not enough operands for the operation.");
            }

            var operand2 = _reversedPolishNotationStack.Pop();
            var operand1 = _reversedPolishNotationStack.Pop();
            var register = Parser.GenerateOperation(operand1, operand2, token);

            _reversedPolishNotationStack.Push(new LexicalToken(LexemType.None, register.Name));
        }
    }
}