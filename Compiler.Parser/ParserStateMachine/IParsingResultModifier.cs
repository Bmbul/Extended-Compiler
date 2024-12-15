using System.Collections.Generic;
using Common;
using Compiler.Generator.Allocator;

namespace Compiler.Parser.ParserStateMachine
{
    public interface IParsingResultModifier
    {
        LexicalToken GetNextToken();
        bool TryGetNextToken(out LexicalToken lexicalToken);
        bool IsInvalidVariableToken(LexicalToken token);
        bool IdentifierExists(string variableValue);
        void ValidateVariableIsDefined(string variableName);
        void SetProgramName(string programName);
        void AddVariables(List<string> identifiers, string dataType);
        void DeclareVariables();
        void AddStatementsSection();
        void GenerateSimpleAssignment(LexicalToken token, LexicalToken firstVariable);
        void GenerateExitWithLastOperationResult();
        Register GenerateOperation(LexicalToken operand1, LexicalToken operand2, LexicalToken operationToken);
        void GenerateWriteCall(LexicalToken printingVariable);
    }
}