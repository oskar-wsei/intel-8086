namespace Emulator.Core.Components.Registers;

public class RegistersComponent
{
    public readonly WordRegisterComponent GeneralA = new();
    public readonly WordRegisterComponent GeneralB = new();
    public readonly WordRegisterComponent GeneralC = new();
    public readonly WordRegisterComponent GeneralD = new();

    public readonly WordRegisterComponent CodeSegment = new();
    public readonly WordRegisterComponent DataSegment = new();
    public readonly WordRegisterComponent StackSegment = new();
    public readonly WordRegisterComponent ExtraSegment = new();

    public readonly WordRegisterComponent InstructionPointer = new();
    public readonly WordRegisterComponent BasePointer = new();
    public readonly WordRegisterComponent StackPointer = new();
    public readonly WordRegisterComponent SourceIndex = new();
    public readonly WordRegisterComponent DestinationIndex = new();

    public readonly FlagRegisterComponent Flags = new();

    public ByteRegisterAccessor GetByteRegister(byte registerId)
    {
        var type = (registerId & 0b100) != 0 ? ByteRegisterType.High : ByteRegisterType.Low;
        var baseRegister = GetWordRegister((byte)(registerId & 0b011));
        return baseRegister.GetByteRegister(type);
    }

    public WordRegisterComponent GetWordRegister(byte registerId)
    {
        return registerId switch
        {
            0b000 => GeneralA,
            0b001 => GeneralC,
            0b010 => GeneralD,
            0b011 => GeneralB,
            0b100 => StackPointer,
            0b101 => BasePointer,
            0b110 => SourceIndex,
            0b111 => DestinationIndex,
            _ => throw new InvalidRegisterException(registerId),
        };
    }

    public WordRegisterComponent GetSegmentRegister(byte registerId)
    {
        return registerId switch
        {
            0b00 => ExtraSegment,
            0b01 => CodeSegment,
            0b10 => StackSegment,
            0b11 => DataSegment,
            _ => throw new InvalidRegisterException(registerId),
        };
    }

    public ushort GetRegisterIndirectAddress(byte rm)
    {
        return rm switch
        {
            0b000 => (ushort)(GeneralB.Value + SourceIndex.Value),
            0b001 => (ushort)(GeneralB.Value + DestinationIndex.Value),
            0b010 => (ushort)(BasePointer.Value + SourceIndex.Value),
            0b011 => (ushort)(BasePointer.Value + DestinationIndex.Value),
            0b100 => SourceIndex.Value,
            0b101 => DestinationIndex.Value,
            0b110 => BasePointer.Value,
            0b111 => GeneralB.Value,
            _ => throw new InvalidRegisterException(rm),
        };
    }

    public void Reset()
    {
        GeneralA.Value = 0;
        GeneralB.Value = 0;
        GeneralC.Value = 0;
        GeneralD.Value = 0;
        CodeSegment.Value = 0;
        DataSegment.Value = 0;
        StackSegment.Value = 0;
        ExtraSegment.Value = 0;
        InstructionPointer.Value = 0;
        BasePointer.Value = 0;
        StackPointer.Value = 0;
        SourceIndex.Value = 0;
        DestinationIndex.Value = 0;
        Flags.Value = 0;
    }
}
