namespace Emulator.Core.Accessors;

public class ImmediateByte : IByteAccessor
{
    public ImmediateByte(byte value = 0)
    {
        Value = value;
    }

    public byte Value { get; set; } = 0;
}
