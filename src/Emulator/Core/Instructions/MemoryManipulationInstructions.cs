using System;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using Emulator.Core.Accessors;
using Emulator.Core.Misc;
using Microsoft.Win32;

namespace Emulator.Core.Instructions;

public class MemoryManipulationInstructions : InstructionHandler
{
    public MemoryManipulationInstructions(VirtualMachine vm) : base(vm) { }

    public override void RegisterInstructions()
    {
        // xchg r/m8, r8
        _vm.RegisterInstruction(0x86, HandleModRmByteInstruction(Exchange));

        // xchg r/m16, r16
        _vm.RegisterInstruction(0x87, HandleModRmWordInstruction(Exchange));

        // mov r/m8, r8
        // mov r/m16, r16
        // mov r8, r/m8
        // mov r16, r/m16
        RegisterModRmInstructions(0x88, Move, Move);

        // mov r/m16, sreg
        _vm.RegisterInstruction(0x8c, HandleModRmWordInstruction(Move, true));

        // lea r16, m
        _vm.RegisterInstruction(0x8d, LoadEffectiveAddress);

        // mov sreg, r/m16
        _vm.RegisterInstruction(0x8e, HandleModRmWordInstruction(Move, true));

        // xchg ax, r16
        for (byte opcode = 0x91; opcode <= 0x97; opcode++)
        {
            _vm.RegisterInstruction(opcode, ExchangeAccumulatorWithWordRegister);
        }

        // sahf
        _vm.RegisterInstruction(0x9e, StoreAccumulatorHighInFlags);

        // lahf
        _vm.RegisterInstruction(0x9f, LoadAccumulatorHighFromFlags);

        // mov al, moffs8
        _vm.RegisterInstruction(0xa0, MoveMemoryByteToAccumulatorLow);

        // mov ax, moffs16
        _vm.RegisterInstruction(0xa1, MoveMemoryWordToAccumulator);

        // mov moffs8, al
        _vm.RegisterInstruction(0xa2, MoveAccumulatorLowToMemoryByte);

        // mov moffs16, ax
        _vm.RegisterInstruction(0xa3, MoveAccumulatorToMemoryWord);

        // movsb
        _vm.RegisterInstruction(0xa4, MoveStringByte);

        // movsw
        _vm.RegisterInstruction(0xa5, MoveStringWord);

        // stosb
        _vm.RegisterInstruction(0xaa, StoreStringByte);

        // stosw
        _vm.RegisterInstruction(0xab, StoreStringWord);

        // lodsb
        _vm.RegisterInstruction(0xac, LoadStringByte);

        // lodsw
        _vm.RegisterInstruction(0xad, LoadStringWord);

        // mov r8, imm8
        for (byte opcode = 0xb0; opcode <= 0xb7; opcode++)
        {
            _vm.RegisterInstruction(opcode, MoveImmediateByteToByteRegister);
        }

        // mov r16, imm16
        for (byte opcode = 0xb8; opcode <= 0xbf; opcode++)
        {
            _vm.RegisterInstruction(opcode, MoveImmediateWordToWordRegister);
        }
    }

    public void Exchange(IByteAccessor dst, IByteAccessor src)
    {
        (dst.Value, src.Value) = (src.Value, dst.Value);
    }

    public void Exchange(IWordAccessor dst, IWordAccessor src)
    {
        (dst.Value, src.Value) = (src.Value, dst.Value);
    }

    public void Move(IByteAccessor dst, IByteAccessor src)
    {
        dst.Value = src.Value;
    }

    public void Move(IWordAccessor dst, IWordAccessor src)
    {
        dst.Value = src.Value;
    }

    public void LoadEffectiveAddress(byte opcode)
    {
        var modRm = new ModRm(_vm.NextByte());
        var address = _vm.Registers.GetRegisterIndirectAddress(modRm.Rm);
        var dst = GetModRmDestinationWordOperand(modRm);
        dst.Value = address;
    }

    public void ExchangeAccumulatorWithWordRegister(byte opcode)
    {
        var accumulator = _vm.Registers.GeneralA;
        var register = GetWordRegisterFromOpcode(opcode);

        (accumulator.Value, register.Value) = (register.Value, accumulator.Value);
    }

    public void StoreAccumulatorHighInFlags(byte opcode)
    {
        _vm.Registers.Flags.SafeSetValue(_vm.Registers.GeneralA.High.Value);
    }

    public void LoadAccumulatorHighFromFlags(byte opcode)
    {
        _vm.Registers.GeneralA.High.Value = (byte)_vm.Registers.Flags.Value;
    }

    public void MoveMemoryByteToAccumulatorLow(byte opcode)
    {
        _vm.Registers.GeneralA.Low.Value = _vm.Memory.GetByte(_vm.NextWord());
    }

    public void MoveMemoryWordToAccumulator(byte opcode)
    {
        _vm.Registers.GeneralA.Value = _vm.Memory.GetWord(_vm.NextWord());
    }

    public void MoveAccumulatorLowToMemoryByte(byte opcode)
    {
        _vm.Memory.SetByte(_vm.NextWord(), _vm.Registers.GeneralA.Low.Value);
    }

    public void MoveAccumulatorToMemoryWord(byte opcode)
    {
        _vm.Memory.SetWord(_vm.NextWord(), _vm.Registers.GeneralA.Value);
    }

    public void MoveStringByte(byte opcode)
    {
        _vm.Memory.SetByte(_vm.Registers.DestinationIndex.Value, _vm.Memory.GetByte(_vm.Registers.SourceIndex.Value));
    }

    public void MoveStringWord(byte opcode)
    {
        _vm.Memory.SetWord(_vm.Registers.DestinationIndex.Value, _vm.Memory.GetWord(_vm.Registers.SourceIndex.Value));
    }

    public void StoreStringByte(byte opcode)
    {
        _vm.Memory.SetByte(_vm.Registers.DestinationIndex.Value, _vm.Registers.GeneralA.Low.Value);
        _vm.Registers.DestinationIndex.Value += (ushort)(_vm.Registers.Flags.Direction ? -1 : 1);
    }

    public void StoreStringWord(byte opcode)
    {
        _vm.Memory.SetWord(_vm.Registers.DestinationIndex.Value, _vm.Registers.GeneralA.Value);
        _vm.Registers.DestinationIndex.Value += (ushort)(_vm.Registers.Flags.Direction ? -2 : 2);
    }

    public void LoadStringByte(byte opcode)
    {
        _vm.Registers.GeneralA.Low.Value = _vm.Memory.GetByte(_vm.Registers.SourceIndex.Value);
        _vm.Registers.SourceIndex.Value += (ushort)(_vm.Registers.Flags.Direction ? -1 : 1);
    }

    public void LoadStringWord(byte opcode)
    {
        _vm.Registers.GeneralA.Value = _vm.Memory.GetWord(_vm.Registers.SourceIndex.Value);
        _vm.Registers.SourceIndex.Value += (ushort)(_vm.Registers.Flags.Direction ? -2 : 2);
    }

    public void MoveImmediateByteToByteRegister(byte opcode)
    {
        GetByteRegisterFromOpcode(opcode).Value = _vm.NextByte();
    }

    public void MoveImmediateWordToWordRegister(byte opcode)
    {
        GetWordRegisterFromOpcode(opcode).Value = _vm.NextWord();
    }
}
