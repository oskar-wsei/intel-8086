namespace Emulator.Core.Components.Registers;

public class FlagRegisterComponent : WordRegisterComponent
{
    protected const ushort CarryBitIndex = 0;
    protected const ushort ParityBitIndex = 2;
    protected const ushort ZeroBitIndex = 6;
    protected const ushort SignBitIndex = 7;
    protected const ushort DirectionBitIndex = 10;
    protected const ushort OverflowBitIndex = 11;

    public bool Carry
    {
        get => GetBit(CarryBitIndex);
        set => SetBit(CarryBitIndex, value);
    }

    public bool Parity
    {
        get => GetBit(ParityBitIndex);
        set => SetBit(ParityBitIndex, value);
    }

    public bool Zero
    {
        get => GetBit(ZeroBitIndex);
        set => SetBit(ZeroBitIndex, value);
    }

    public bool Sign
    {
        get => GetBit(SignBitIndex);
        set => SetBit(SignBitIndex, value);
    }

    public bool Direction
    {
        get => GetBit(DirectionBitIndex);
        set => SetBit(DirectionBitIndex, value);
    }

    public bool Overflow
    {
        get => GetBit(OverflowBitIndex);
        set => SetBit(OverflowBitIndex, value);
    }

    public byte CarryValue { get => (byte)(Carry ? 1 : 0); }
    public byte ParityValue { get => (byte)(Parity ? 1 : 0); }
    public byte ZeroValue { get => (byte)(Zero ? 1 : 0); }
    public byte SignValue { get => (byte)(Sign ? 1 : 0); }
    public byte DirectionValue { get => (byte)(Direction ? 1 : 0); }
    public byte OverflowValue { get => (byte)(Overflow ? 1 : 0); }

    public void ClearCarry() => Carry = false;
    public void SetCarry(bool value = true) => Carry = value;

    public void ClearParity() => Parity = false;
    public void SetParity(bool value = true) => Parity = value;

    public void ClearZero() => Zero = false;
    public void SetZero(bool value = true) => Zero = value;

    public void ClearSign() => Sign = false;
    public void SetSign(bool value = true) => Sign = value;

    public void ClearDirection() => Direction = false;
    public void SetDirection(bool value = true) => Direction = value;

    public void ClearOverflow() => Overflow = false;
    public void SetOverflow(bool value = true) => Overflow = value;

    public void SafeSetValue(ushort value)
    {
        ushort ignoreMask = 0b00001000_00110101;
        value &= ignoreMask;
        Value = (ushort)((Value & (~ignoreMask)) | value);
    }

    protected bool GetBit(ushort index)
    {
        return ((Value >> index) & 1) == 1;
    }

    protected void SetBit(ushort index, bool value)
    {
        Value = (ushort)(((value ? 1 : 0) << index) | (Value & ~(1 << index)));
    }
}
