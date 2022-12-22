namespace Emulator.Core.Misc;

public class ModRm
{
    public byte Value { get; protected set; }

    public ModRm(byte value)
    {
        Value = value;
    }

    public byte Mod
    {
        get => (byte)((Value & 0b11000000) >> 6);
    }

    public byte Reg
    {
        get => (byte)((Value & 0b111000) >> 3);
    }

    public byte Rm
    {
        get => (byte)(Value & 0b111);
    }
}
