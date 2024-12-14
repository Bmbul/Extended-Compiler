namespace Compiler.Parser.Exceptions
{
    public class DuplicateVariableNameException : ParsingException
    {
        public DuplicateVariableNameException() : base("Duplicate variable name.")
        {
            
        }
    }
}