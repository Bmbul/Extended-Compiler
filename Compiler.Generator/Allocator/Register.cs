namespace Compiler.Generator.Allocator
{
    public class Register
    {
        public Register(string name)
        {
            Name = name;
            IsUsed = false;
        }

        public string Name { get; set; }
    
        public bool IsUsed { get; set; }
    }
}