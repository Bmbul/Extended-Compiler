namespace Compiler.Generator.Allocator
{
    public interface IRegisterAllocator
    {
        Register Allocate(bool use = false);

        Register GetByName(string name);

        void Deallocate(string name);

        void DeallocateAll();
    }
}