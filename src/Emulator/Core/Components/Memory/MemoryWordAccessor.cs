using Emulator.Core.Accessors;

namespace Emulator.Core.Components.Memory;

public class MemoryWordAccessor : MemoryAccessor, IWordAccessor
{
    public MemoryWordAccessor(MemoryComponent memory, uint address) : base(memory, address) { }

    public ushort Value
    {
        get => _memory.GetWord(_address);
        set => _memory.SetWord(_address, value);
    }
}
