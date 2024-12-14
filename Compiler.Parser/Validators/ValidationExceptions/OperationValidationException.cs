using Compiler.Parser.Exceptions;

namespace Compiler.Parser.Validators.ValidationExceptions
{
    public class OperationValidationException : ParsingException
    {
        public OperationValidationException(string explanation) : base($"Invalid operation exception: {explanation}")
        {
        
        }
    }
}