namespace Compiler.Parser.Exceptions
{
    public class FunctionRedefinitionException : ParsingException
    {
        public FunctionRedefinitionException(string functionName) : base($"Function Redefinition: {functionName}.")
        {
            
        }
    }
}