using System;
using System.Collections.Generic;
using Compiler.Generator.Exceptions;

namespace Compiler.Generator.Allocator
{
    public class RegisterAllocator : IRegisterAllocator
    {
        private readonly List<Register> _registers;
        public RegisterAllocator()
        {
            _registers = new List<Register>();
            foreach (var registerName in CodeGeneratorConstants.RegisterNames)
            {
                _registers.Add(new Register(registerName));
            }
        }
        
        
        public Register Allocate(bool use = false)
        {
            foreach (var register in _registers)
            {
                if (!register.IsUsed)
                {
                    register.IsUsed = use;
                    return register;
                }
            }

            throw new AllocatorException("Cannot allocate any register. All the registers are in use.");
        }

        public Register GetByName(string name)
        {
            foreach (var register in _registers)
            {
                if (register.Name != name) 
                    continue;

                return register;
            }
            throw new AllocatorException($"Cound not find the register with name: {name}.");
        }

        public void Deallocate(string name)
        {
            var register = GetByName(name);
            register.IsUsed = false;
        }

        public void DeallocateAll()
        {
            foreach (var register in _registers)
            {
                register.IsUsed = false;
            }        
        }
    }
}