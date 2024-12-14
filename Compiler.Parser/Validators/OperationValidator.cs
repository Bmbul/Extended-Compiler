using Common;
using Common.Utility;
using Compiler.Parser.Validators.ValidationExceptions;

namespace Compiler.Parser.Validators
{
    public class OperationValidator : IOperationValidator
    {
        private readonly string plusKeyword = Constants.TypesToLexem[LexicalTokensEnum.Plus];
        private readonly string stringKeyword = Constants.TypesToLexem[LexicalTokensEnum.String];
        private readonly string integerKeyword = Constants.TypesToLexem[LexicalTokensEnum.Integer];
        public void ValidateAssignment(string resultType, string assigneeType)
        {
            if (resultType != assigneeType)
            {
                throw new OperationValidationException($"Type mismatch: typeof({resultType}) != typeof({assigneeType}).");
            }
        }

        public string ValidateOperation(string firstType, string secondType, LexicalToken operation)
        {
            string resultType = null;
            if (operation.IsSpecialToken(plusKeyword))
            {
                if (firstType == secondType)
                {
                    resultType = firstType;
                }
                else if (firstType == stringKeyword || secondType == stringKeyword)
                {
                    resultType = stringKeyword;
                }
            }
            else if(firstType == secondType && firstType == integerKeyword)
            {
                {
                    resultType = integerKeyword;
                }
            }
            
            if (resultType == null)
            {
                throw new OperationValidationException( $"Type mismatch: {firstType} {operation.Value} {secondType}");
            }
            return resultType;
        }
    }
}