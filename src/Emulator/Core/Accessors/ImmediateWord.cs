namespace Emulator.Core.Accessors;

public class ImmediateWord : IWordAccessor
{
    public ImmediateWord(ushort value = 0)
    {
        Value = value;

    }

    public ushort Value { get; set; } = 0;
}
