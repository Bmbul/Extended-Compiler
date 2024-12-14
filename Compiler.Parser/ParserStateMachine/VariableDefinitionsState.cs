using System;
using System.Collections.Generic;
using Common;
using Common.Utility;
using Compiler.Parser.Exceptions;

namespace Compiler.Parser.ParserStateMachine
{
    public class VariableDefinitionsState : ParsingStateBase
    {
        private readonly bool _isOptionalDeclaration;

        public VariableDefinitionsState(IParsingResultModifier parser, bool isOptionalDeclaration = false)
            : base(parser)
        {
            _isOptionalDeclaration = isOptionalDeclaration;
        }

        public override IParsingState Handle(LexicalToken token)
        {
            List<string> identifiers = new List<string>();
            
            while (true)
            {
                if (!token.IsIdentifier())
                {
                    if (!_isOptionalDeclaration)
                    {
                        throw new ParsingException("Identifier expected.");
                    }
                    return IdentifyNextState(token);
                }

                if (identifiers.Contains(token.Value))
                {
                    throw new DuplicateVariableNameException();
                }
                identifiers.Add(token.Value);
                
                var nextToken = Parser.GetNextToken();
                if (nextToken.IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.Comma])) // identifier,
                {
                    token = Parser.GetNextToken();
                }
                else if (nextToken.IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.Colon])) // identifier :
                {
                    nextToken = Parser.GetNextToken();
                    if (nextToken.IsDataType())
                    {
                        Parser.AddVariables(identifiers, nextToken.Value);
                    }

                    if (!Parser.GetNextToken().IsSpecialToken(Constants.TypesToLexem[LexicalTokensEnum.Semicolon]))
                    {
                        throw new ParsingException("Semicolon expected!");
                    }
                    return new VariableDefinitionsState(Parser, true);
                }
            }
        }

        private IParsingState IdentifyNextState(LexicalToken token)
        {
            if (token.IsKeyword(Constants.TypesToLexem[LexicalTokensEnum.VarKeyword]))
            {
                return new VariableDefinitionsState(Parser);
            }
            
            if (!token.IsKeyword(Constants.TypesToLexem[LexicalTokensEnum.BeginKeyword]))
            {
                throw new ParsingException("Expected variable declarations or begin keyword.");
            }
            Parser.DeclareVariables();
            return new StatementSequenceState(Parser);
        }
    }
}