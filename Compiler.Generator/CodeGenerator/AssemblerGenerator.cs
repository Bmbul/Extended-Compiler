using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using Common.Utility;

namespace Compiler.Generator.CodeGenerator
{
    public class AssemblerGenerator : IAssemblerGenerator
    {
        private readonly StringBuilder _assemblerCode;
        private readonly string _programName;
        
        public AssemblerGenerator(string programName)
        {
            _programName = programName;
            _assemblerCode = new StringBuilder();
        }

        public void DeclareVariables(Dictionary<string, string> variables)
        {
            if (variables.Count <= 0)
            {
                Console.WriteLine("No variables declared");
                return;
            }
            _assemblerCode.AppendLine(".section .bss");
            foreach (var variable in variables)
            {
                if (variable.Value == Constants.TypesToLexem[LexicalTokensEnum.Integer])
                {
                    _assemblerCode.AppendLine($".lcomm {_programName}_{variable.Key}, 8");
                }
                else if (variable.Value == Constants.TypesToLexem[LexicalTokensEnum.String])
                {
                    _assemblerCode.AppendLine($".lcomm {_programName}_{variable.Key}, 256");
                }
            }

            _assemblerCode.AppendLine();
        }

        public void AddGlobalAndTextSection()
        {
            _assemblerCode.AppendLine(".section .text");
            _assemblerCode.AppendLine(".global _start");
            _assemblerCode.AppendLine("_start:");
        }
        
        public void GenerateExitWithLastOperationResult()
        {
            _assemblerCode.AppendLine("\n\tMOVQ %rax, %rdi");
            _assemblerCode.AppendLine("\tMOVQ $60, %rax");
            _assemblerCode.AppendLine("\tSYSCALL");
        }

        public void GenerateSimpleAssignment(string destination, string source)
        {
            _assemblerCode.AppendLine($"\tMOVQ {source}, {destination}");
        }

        public void GenerateAssignmentAfterOperation(string destination, string firstVar, string secondVar, string operation)
        {
            _assemblerCode.AppendLine($"\tMOVQ {firstVar}, %rax");

            if (operation == Constants.TypesToLexem[LexicalTokensEnum.Plus])
            {
                _assemblerCode.AppendLine($"\tADDQ {secondVar}, %rax");
            }
            else if (operation == Constants.TypesToLexem[LexicalTokensEnum.Minus])
            {

                _assemblerCode.AppendLine(
                    $"\tSUBQ {secondVar}, %rax");
            }
            else
            {
                throw new NotImplementedException("Unsupported operation");
            }
            _assemblerCode.AppendLine($"\tMOVQ %rax, {destination}");
        }
        
        public void GetGeneratedCode(string outputFileName)
        {
            File.WriteAllText(outputFileName, _assemblerCode.ToString());
        }
    }
}