using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Compiler.Parser.Exceptions;

namespace Compiler.Parser.ParserStateMachine
{
    public class ParsingResult
    {
        public string ProgramName { get; set; }
        public Dictionary<string, VariableInformation> Variables = new Dictionary<string, VariableInformation>();
        
        public void DeclareVariables(List<string> identifiers, string dataType)
        {
            foreach (var identifier in identifiers)
            {
                if (identifier.IsKeyword())
                {
                    throw new ParsingException($"Variable name Exception. Variable name cannot be a keyword: {identifier}");
                }
                if (Variables.ContainsKey(identifier))
                {
                    throw new DuplicateVariableNameException();
                }

                Variables[identifier] = new VariableInformation(dataType);
            }
        }
        
        public void SetAssigned(string variableName)
        {
            Variables[variableName].SetDefined();
        }

        public bool IsAssigned(string variableName)
        {
            return Variables[variableName].IsInitialized;
        }

        public Dictionary<string, string> GetVariables()
        {
            return Variables.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Type);
        }
    }
    
    public class VariableInformation
    {
        public VariableInformation(string varType)
        {
            Type = varType;
        }
        
        public string Type { get; }
        public bool IsInitialized { get; private set; }
        
        public void SetDefined()
        {
            IsInitialized = true;
        }
    }
}