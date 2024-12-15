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
        private readonly string minusOperator = Constants.TypesToLexem[LexicalTokensEnum.Minus];
        private readonly string plusOperator = Constants.TypesToLexem[LexicalTokensEnum.Plus];
        private readonly string multiplicationOperator = Constants.TypesToLexem[LexicalTokensEnum.Multiplication];

        public AssemblerGenerator(string programName)
        {
            _programName = programName;
            _registerAllocator = new RegisterAllocator();
            _assemblerCode = new StringBuilder();
        }

        public void DeclareVariables(Dictionary<string, string> variables)
        {
            AddRoDataSection();
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

        public void AddRoDataSection()
        {
            _assemblerCode.AppendLine(".section .rodata");
            _assemblerCode.AppendLine("fmt:");
            _assemblerCode.AppendLine("\t.asciz \"%d\\n\"");
            _assemblerCode.AppendLine();
        }

        public void GenerateExitWithLastOperationResult()
        {
            _assemblerCode.AppendLine("\n\tMOVQ %rax, %rdi");
            _assemblerCode.AppendLine("\tMOVQ $60, %rax");
            _assemblerCode.AppendLine("\tSYSCALL");
        }

        public void GenerateSimpleAssignment(LexicalToken destination, LexicalToken source)
        {
            var sourceNaming = GetVariableAssemblerNaming(source);
            var destinationNaming = GetVariableAssemblerNaming(destination);
            _assemblerCode.AppendLine($"\tMOVQ {sourceNaming}, {destinationNaming}");
        }

        private string GetVariableAssemblerNaming(LexicalToken token)
        {
            if (token.Type == LexemType.None)
            {
                return $"%{token.Value}";
            }
            
            return token.IsNumber() ? $"${token.Value}"
                : $"{_programName}_{token.Value}";
        }
        
        public void GetGeneratedCode(string outputFileName)
        {
            File.WriteAllText(outputFileName, _assemblerCode.ToString());
        }

        public Register GenerateOperation(LexicalToken operand1, LexicalToken operand2, LexicalToken operatorToken)
        {
            Register register = null;
            var operation = operatorToken.Value;
            if (operand1.Type == LexemType.None)
            {
                register = _registerAllocator.GetByName(operand1.Value);
            }
            else
            {
                register = _registerAllocator.Allocate(true);
                var operand1Naming = GetVariableAssemblerNaming(operand1);
                _assemblerCode.AppendLine($"\tMOVQ {operand1Naming}, %{register.Name}");
            }

            var operand2Naming = GetVariableAssemblerNaming(operand2);
            if (operation == plusOperator)
            {
                _assemblerCode.AppendLine($"\tADDQ {operand2Naming}, %{register.Name}");
            }
            else if(operation == minusOperator)
            {
                _assemblerCode.AppendLine($"\tSUBQ {operand2Naming}, %{register.Name}");
            }
            else if (operation == multiplicationOperator)
            {
                _assemblerCode.AppendLine($"\tIMULQ {operand2Naming}, %{register.Name}");
            }
            else
            {
                throw new NotImplementedException("Unsupported operation");
            }
            
            if (operand2.Type == LexemType.None)
            {
                _registerAllocator.Deallocate(operand2.Value);
            }

            return register;
        }
    }
}