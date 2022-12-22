using System;
using Emulator.Core.Accessors;
using Emulator.Core.Utils;
using Microsoft.Win32;

namespace Emulator.Core.Instructions;

public class FlowControlInstructions : InstructionHandler
{
    public FlowControlInstructions(VirtualMachine vm) : base(vm) { }

    public override void RegisterInstructions()
    {
        var flags = _vm.Registers.Flags;

        // jo rel8
        _vm.RegisterInstruction(0x70, (opcode) => PerformConditionalJumpShort(flags.Overflow));

        // jno rel8
        _vm.RegisterInstruction(0x71, (opcode) => PerformConditionalJumpShort(!flags.Overflow));

        // jc rel8
        _vm.RegisterInstruction(0x72, (opcode) => PerformConditionalJumpShort(flags.Carry));

        // jnc rel8
        _vm.RegisterInstruction(0x73, (opcode) => PerformConditionalJumpShort(!flags.Carry));

        // jz rel8
        _vm.RegisterInstruction(0x74, (opcode) => PerformConditionalJumpShort(flags.Zero));

        // jnz rel8
        _vm.RegisterInstruction(0x75, (opcode) => PerformConditionalJumpShort(!flags.Zero));

        // ja rel8
        _vm.RegisterInstruction(0x76, (opcode) => PerformConditionalJumpShort(!flags.Carry && !flags.Zero));

        // jna rel8
        _vm.RegisterInstruction(0x77, (opcode) => PerformConditionalJumpShort(flags.Carry || flags.Zero));

        // js rel8
        _vm.RegisterInstruction(0x78, (opcode) => PerformConditionalJumpShort(flags.Sign));

        // jns rel8
        _vm.RegisterInstruction(0x79, (opcode) => PerformConditionalJumpShort(!flags.Sign));

        // jpe rel8
        _vm.RegisterInstruction(0x7a, (opcode) => PerformConditionalJumpShort(flags.Parity));

        // jpo rel8
        _vm.RegisterInstruction(0x7b, (opcode) => PerformConditionalJumpShort(!flags.Parity));

        // jl rel8
        _vm.RegisterInstruction(0x7c, (opcode) => PerformConditionalJumpShort(flags.Sign != flags.Overflow));

        // jnl rel8
        _vm.RegisterInstruction(0x7d, (opcode) => PerformConditionalJumpShort(flags.Sign == flags.Overflow));

        // jng rel8
        _vm.RegisterInstruction(0x7e, (opcode) => PerformConditionalJumpShort(flags.Zero || flags.Sign != flags.Overflow));

        // jg rel8
        _vm.RegisterInstruction(0x7f, (opcode) => PerformConditionalJumpShort(!flags.Zero && flags.Sign == flags.Overflow));

        // ret imm16
        _vm.RegisterInstruction(0xc2, PerformReturnNearPopImmediateWord);

        // ret
        _vm.RegisterInstruction(0xc3, PerformReturnNear);

        // jcxz rel8
        _vm.RegisterInstruction(0xe3, (opcode) => PerformConditionalJumpShort(_vm.Registers.GeneralC.Value == 0));

        // call rel16
        _vm.RegisterInstruction(0xe8, PerformCallNearRelative);

        // jmp rel16
        _vm.RegisterInstruction(0xe9, PerformJumpNearRelative);

        // jmp rel8
        _vm.RegisterInstruction(0xeb, PerformJumpShortRelative);
    }

    public void PerformReturnNearPopImmediateWord(byte opcode)
    {
        PerformReturnNear(opcode);
        _vm.Registers.StackPointer.Value += _vm.NextWord();
    }

    public void PerformReturnNear(byte opcode)
    {
        _vm.Registers.InstructionPointer.Value = PopWord();
    }

    public void PerformCallNearRelative(byte opcode)
    {
        var offset = _vm.NextWord();
        PerformCallNearAbsolute((ushort)(_vm.Registers.InstructionPointer.Value + offset));
    }

    public void PerformCallNearAbsolute(IWordAccessor address)
    {
        PerformCallNearAbsolute(address.Value);
    }

    public void PerformCallNearAbsolute(ushort address)
    {
        PushWord(_vm.Registers.InstructionPointer.Value);
        PerformJumpNearAbsolute(address);
    }

    public void PerformJumpNearRelative(byte opcode)
    {
        var nextInstructionAddress = (ushort)(_vm.Registers.InstructionPointer.Value + 2); // 2 = 2 byte operand
        _vm.Registers.InstructionPointer.Value = (ushort)(nextInstructionAddress + _vm.NextWord());
    }

    public void PerformJumpShortRelative(byte opcode)
    {
        PerformConditionalJumpShort(true);
    }

    public void PerformJumpNearAbsolute(IWordAccessor address)
    {
        PerformJumpNearAbsolute(address.Value);
    }

    public void PerformJumpNearAbsolute(ushort address)
    {
        _vm.Registers.InstructionPointer.Value = address;
    }

    public void PerformConditionalJumpShort(bool condition)
    {
        var offset = _vm.NextByte();

        if (condition)
        {
            _vm.Registers.InstructionPointer.Value = ArithmeticUtils.AddSigned(_vm.Registers.InstructionPointer.Value, offset);
        }

    }
}
