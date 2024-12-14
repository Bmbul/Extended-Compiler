using System.Collections.Generic;

namespace Compiler.Generator.CodeGenerator
{
    public interface IAssemblerGenerator
    {
        void DeclareVariables(Dictionary<string, string> variables);
        void AddGlobalAndTextSection();
        void GenerateExitWithLastOperationResult();
        void GenerateSimpleAssignment(string destination, string source);
        void GenerateAssignmentAfterOperation(string destination, string firstVar, string secondVar, string operation);
        void GetGeneratedCode(string outputFileName);
    }
}