namespace Emulator.Core.Components.Memory;

public abstract class MemoryAccessor
{
    protected readonly MemoryComponent _memory;
    protected readonly uint _address;

    public MemoryAccessor(MemoryComponent memory, uint address)
    {
        _memory = memory;
        _address = address;
    }
}
