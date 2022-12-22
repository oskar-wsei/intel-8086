namespace Emulator.Core.Components.Memory;

public class MemoryComponent
{
    public readonly uint Size;
    protected readonly byte[] _data;

    public MemoryComponent(uint size)
    {
        Size = size;
        _data = new byte[Size];
    }

    public void SetByte(uint address, byte value)
    {
        CheckAddress(address);
        _data[address] = value;
    }

    public byte GetByte(uint address)
    {
        CheckAddress(address);
        return _data[address];
    }

    public void SetWord(uint address, ushort value)
    {
        CheckAddress(address + 1);
        _data[address + 1] = (byte)(value >> 8);
        _data[address + 0] = (byte)value;
    }

    public ushort GetWord(uint address)
    {
        CheckAddress(address + 1);
        return (ushort)(_data[address + 1] << 8 | _data[address]);
    }

    public void Reset()
    {
        for (int i = 0; i < Size; i++)
        {
            _data[i] = 0;
        }
    }

    protected void CheckAddress(uint address)
    {
        if (address >= Size) throw new MemoryAccessException(address);
    }
}
