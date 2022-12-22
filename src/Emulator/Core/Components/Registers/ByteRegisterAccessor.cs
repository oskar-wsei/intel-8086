using Emulator.Core.Accessors;

namespace Emulator.Core.Components.Registers;

public class ByteRegisterAccessor : IByteAccessor
{
    protected readonly WordRegisterComponent _baseRegister;
    protected readonly ByteRegisterType _type;

    public ByteRegisterAccessor(WordRegisterComponent baseRegister, ByteRegisterType type)
    {
        _baseRegister = baseRegister;
        _type = type;
    }

    public byte Value
    {
        get
        {
            return _type == ByteRegisterType.Low ? (byte)_baseRegister.Value : (byte)((_baseRegister.Value & 0xff00) >> 8);
        }
        set
        {
            if (_type == ByteRegisterType.Low)
            {
                _baseRegister.Value = (ushort)(value | (_baseRegister.Value & 0xff00));
            }
            else
            {
                _baseRegister.Value = (ushort)((value << 8) | (_baseRegister.Value & 0x00ff));
            }
        }
    }
}
