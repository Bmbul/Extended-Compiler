namespace Compiler.Generator.Allocator
{
    public interface IRegisterAllocator
    {
        Register Allocate(bool use = false);

        void Deallocate(string name);

        void DeallocateAll();
    }
}