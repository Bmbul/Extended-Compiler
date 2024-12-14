using System.Collections.Generic;

namespace Compiler.Generator
{
    public static class CodeGeneratorConstants
    {
        public static readonly List<string> RegisterNames = new List<string>
        {
            "rax",
            "rbx",
            "rcx",
            "rdx",
            "rsi",
            "rdi",
            "rbp",
            "rsp",
            "r8",
            "r9",
            "r10",
            "r11", 
            "r12",
            "r13",
            "r14",
            "r15",
        };
    }
}