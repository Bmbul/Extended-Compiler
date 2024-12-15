using System.Collections.Generic;
using Common;
using Compiler.Generator.Allocator;

namespace Compiler.Generator.CodeGenerator
{
    public interface IAssemblerGenerator
    {
        void DeclareVariables(Dictionary<string, string> variables);
        void AddGlobalAndTextSection();
        void AddRoDataSection();
        void GenerateExitWithLastOperationResult();
        void GenerateSimpleAssignment(LexicalToken destination, LexicalToken source);
        void GetGeneratedCode(string outputFileName);
        Register GenerateOperation(LexicalToken operand1, LexicalToken operand2, LexicalToken operatorToken);
    }
}