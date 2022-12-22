using Emulator.Core.Accessors;

namespace Emulator.Core.Components.Memory;

public class MemoryByteAccessor : MemoryAccessor, IByteAccessor
{
    public MemoryByteAccessor(MemoryComponent memory, uint address) : base(memory, address) { }

    public byte Value
    {
        get => _memory.GetByte(_address);
        set => _memory.SetByte(_address, value);
    }
}
