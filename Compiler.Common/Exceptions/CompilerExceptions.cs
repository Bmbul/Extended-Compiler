using System;

namespace Common.Exceptions
{
    public class CompilerExceptions : ApplicationException
    {
        public CompilerExceptions(string exception) 
            : base(exception)
        {
        
        }
    }
}