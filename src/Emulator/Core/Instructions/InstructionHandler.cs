using Emulator.Core.Accessors;
using Emulator.Core.Components.Memory;
using Emulator.Core.Components.Registers;
using Emulator.Core.Misc;
using Emulator.Core.Utils;
using System;

namespace Emulator.Core.Instructions;

public abstract class InstructionHandler
{
    protected readonly VirtualMachine _vm;

    public InstructionHandler(VirtualMachine vm)
    {
        _vm = vm;
    }

    public abstract void RegisterInstructions();

    protected void RegisterModRmInstructions(
        byte firstOpcode,
        Action<IByteAccessor, IByteAccessor> byteHandler,
        Action<IWordAccessor, IWordAccessor> wordHandler
    )
    {
        _vm.RegisterInstruction((byte)(firstOpcode + 0), HandleModRmByteInstruction(byteHandler));
        _vm.RegisterInstruction((byte)(firstOpcode + 1), HandleModRmWordInstruction(wordHandler));
        _vm.RegisterInstruction((byte)(firstOpcode + 2), HandleModRmByteInstruction(byteHandler));
        _vm.RegisterInstruction((byte)(firstOpcode + 3), HandleModRmWordInstruction(wordHandler));
    }

    protected void RegisterAccumulatorImmediateInstructions(
        byte firstOpcode,
        Action<IByteAccessor, IByteAccessor> byteHandler,
        Action<IWordAccessor, IWordAccessor> wordHandler
    )
    {
        _vm.RegisterInstruction((byte)(firstOpcode + 0), HandleAccumulatorImmediateByteInstruction(byteHandler));
        _vm.RegisterInstruction((byte)(firstOpcode + 1), HandleAccumulatorImmediateWordInstruction(wordHandler));
    }

    protected Action<byte> HandleAccumulatorImmediateByteInstruction(Action<IByteAccessor, IByteAccessor> handler)
    {
        return delegate (byte opcode)
        {
            handler(_vm.Registers.GeneralA.Low, new ImmediateByte(_vm.NextByte()));
        };
    }

    protected Action<byte> HandleAccumulatorImmediateWordInstruction(Action<IWordAccessor, IWordAccessor> handler)
    {
        return delegate (byte opcode)
        {
            handler(_vm.Registers.GeneralA, new ImmediateWord(_vm.NextWord()));
        };
    }

    protected Action<byte> HandleModRmByteInstruction(Action<IByteAccessor, IByteAccessor> handler)
    {
        return delegate (byte opcode)
        {
            var modRm = new ModRm(_vm.NextByte());
            var (dst, src) = GetModRmByteOperands(opcode, modRm);
            handler(dst, src);
        };
    }

    protected Action<byte> HandleModRmWordInstruction(Action<IWordAccessor, IWordAccessor> handler, bool useSegment = false)
    {
        return delegate (byte opcode)
        {
            var modRm = new ModRm(_vm.NextByte());
            var (dst, src) = GetModRmWordOperands(opcode, modRm, useSegment);
            handler(dst, src);
        };
    }

    protected (IByteAccessor, IByteAccessor) GetModRmByteOperands(byte opcode, ModRm modRm)
    {
        IByteAccessor dst = GetModRmDestinationByteOperand(modRm);
        IByteAccessor src = _vm.Registers.GetByteRegister(modRm.Reg);

        var directionFlagSet = (opcode & 0b10) != 0;
        return directionFlagSet ? (src, dst) : (dst, src);
    }

    protected (IWordAccessor, IWordAccessor) GetModRmWordOperands(byte opcode, ModRm modRm, bool useSegment = false)
    {
        IWordAccessor dst = GetModRmDestinationWordOperand(modRm);
        IWordAccessor src = useSegment ?
                _vm.Registers.GetSegmentRegister(modRm.Reg) :
                _vm.Registers.GetWordRegister(modRm.Reg);

        var directionFlagSet = (opcode & 0b10) != 0;
        return directionFlagSet ? (src, dst) : (dst, src);
    }

    protected IByteAccessor GetModRmDestinationByteOperand(ModRm modRm)
    {
        IByteAccessor dst;

        if (modRm.Mod == 0b11)
        {
            // Register addressing mode

            dst = _vm.Registers.GetByteRegister(modRm.Rm);
        }
        else
        {
            // Register indirect addressing with optional signed dispacement
            // or 16-bit displacement addressing only

            dst = new MemoryByteAccessor(_vm.Memory, GetModRmAddress(modRm));
        }

        return dst;
    }

    protected IWordAccessor GetModRmDestinationWordOperand(ModRm modRm)
    {
        IWordAccessor dst;

        if (modRm.Mod == 0b11)
        {
            // Register addressing mode

            dst = _vm.Registers.GetWordRegister(modRm.Rm);
        }
        else
        {
            // Register indirect addressing with optional signed dispacement
            // or 16-bit displacement addressing only

            dst = new MemoryWordAccessor(_vm.Memory, GetModRmAddress(modRm));
        }

        return dst;
    }

    protected ushort GetModRmAddress(ModRm modRm)
    {
        if (modRm.Mod == 0b00 && modRm.Rm == 0b110)
        {
            // Two-byte address only
            return _vm.NextWord();
        }

        // Indirect addressing mode

        ushort address = _vm.Registers.GetRegisterIndirectAddress(modRm.Rm);

        if (modRm.Mod == 0b01)
        {
            // address + 8-bit displacement
            address = ArithmeticUtils.AddSigned(address, _vm.NextByte());
        }
        else if (modRm.Mod == 0b10)
        {
            // address + 16-bit displacement
            address += _vm.NextWord();
        }

        return address;
    }

    protected byte GetRegisterIdFromOpcode(byte opcode)
    {
        return (byte)(opcode & 0b00000111);
    }

    protected ByteRegisterAccessor GetByteRegisterFromOpcode(byte opcode)
    {
        return _vm.Registers.GetByteRegister(GetRegisterIdFromOpcode(opcode));
    }

    protected WordRegisterComponent GetWordRegisterFromOpcode(byte opcode)
    {
        return _vm.Registers.GetWordRegister(GetRegisterIdFromOpcode(opcode));
    }

    protected void PushByte(byte value)
    {
        PushWord(ArithmeticUtils.SignExtend(value));
    }

    protected byte PopByte()
    {
        return (byte)PopWord();
    }

    protected void PushWord(ushort value)
    {
        _vm.Registers.StackPointer.Value -= 2;
        _vm.Memory.SetWord(_vm.Registers.StackPointer.Value, value);
    }

    protected ushort PopWord()
    {
        ushort value = _vm.Memory.GetWord(_vm.Registers.StackPointer.Value);
        _vm.Registers.StackPointer.Value += 2;
        return value;
    }
}
