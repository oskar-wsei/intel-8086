using Emulator.Core.Utils;

namespace Emulator.Core.Components.Memory;

public class MemoryAccessException : EmulationException
{
    public MemoryAccessException(uint address)
        : base($"Cannot access memory address ${FormatUtils.ToHex(address)}") { }
}
