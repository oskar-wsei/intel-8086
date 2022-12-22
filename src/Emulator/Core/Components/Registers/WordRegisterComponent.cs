using Emulator.Core.Accessors;

namespace Emulator.Core.Components.Registers;

public class WordRegisterComponent : IWordAccessor
{
    public ushort Value { get; set; } = 0;

    public ByteRegisterAccessor Low { get; private set; }
    public ByteRegisterAccessor High { get; private set; }

    public WordRegisterComponent()
    {
        Low = new ByteRegisterAccessor(this, ByteRegisterType.Low);
        High = new ByteRegisterAccessor(this, ByteRegisterType.High);
    }

    public ByteRegisterAccessor GetByteRegister(ByteRegisterType type)
    {
        return type == ByteRegisterType.Low ? Low : High;
    }

    public void Clear() => Value = 0;
}
