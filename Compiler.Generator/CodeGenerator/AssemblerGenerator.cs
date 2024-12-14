using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Common;
using Common.Utility;
using Compiler.Generator.Allocator;

namespace Compiler.Generator.CodeGenerator
{
    public class AssemblerGenerator : IAssemblerGenerator
    {
        private readonly StringBuilder _assemblerCode;
        private readonly IRegisterAllocator _registerAllocator;
        private readonly string _programName;
        
        public AssemblerGenerator(string programName)
        {
            _programName = programName;
            _registerAllocator = new RegisterAllocator();
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
            _assemblerCode.AppendLine(".globl _start");
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
            var register = _registerAllocator.Allocate().Name;
            _assemblerCode.AppendLine($"\tMOVQ {firstVar}, %{register}");

            if (operation == Constants.TypesToLexem[LexicalTokensEnum.Plus])
            {
                _assemblerCode.AppendLine($"\tADDQ {secondVar}, %{register}");
            }
            else if (operation == Constants.TypesToLexem[LexicalTokensEnum.Minus])
            {
                _assemblerCode.AppendLine($"\tSUBQ {secondVar}, %{register}");
            }
            else
            {
                throw new NotImplementedException("Unsupported operation");
            }
            _assemblerCode.AppendLine($"\tMOVQ %{register}, {destination}");
        }
        
        public void GetGeneratedCode(string outputFileName)
        {
            File.WriteAllText(outputFileName, _assemblerCode.ToString());
        }

        public Register GenerateOperation(LexicalToken operand1, LexicalToken operand2, LexicalToken token)
        {
            var register = _registerAllocator.Allocate(true);

            return register;
        }
    }
}