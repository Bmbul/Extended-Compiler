namespace Common.Utility
{
    public struct VariableDefinition
    {
        public VariableDefinition(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
    
        public string Type { get; }
    }
}