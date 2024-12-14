using Common.Exceptions;

namespace Compiler.Generator.Exceptions
{
    public class AllocatorException : CompilerExceptions
    {
        public AllocatorException(string exception) : base(exception)
        {
        }
    }
}