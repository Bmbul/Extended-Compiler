using System;
using Common;
using Common.Exceptions;

namespace Compiler.Parser.Exceptions
{
    public class ParsingException : CompilerExceptions
    {
        public ParsingException(string exception) 
            : base(exception)
        {
        
        }
        
        public ParsingException(string exception, LexicalToken token) 
            : base($"{exception} | Got {token}")
        {
            
        }
    }
}