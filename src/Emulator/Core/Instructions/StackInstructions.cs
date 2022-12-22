using Emulator.Core.Accessors;
using Emulator.Core.Misc;

namespace Emulator.Core.Instructions;

public class StackInstructions : InstructionHandler
{
    public StackInstructions(VirtualMachine vm) : base(vm) { }

    public override void RegisterInstructions()
    {
        // push es
        _vm.RegisterInstruction(0x06, PushExtraSegment);

        // pop es
        _vm.RegisterInstruction(0x07, PopExtraSegment);

        // push cs
        _vm.RegisterInstruction(0x0e, PushCodeSegment);

        // pop cs
        _vm.RegisterInstruction(0x0f, PopCodeSegment);

        // push ss
        _vm.RegisterInstruction(0x16, PushStackSegment);

        // pop ss
        _vm.RegisterInstruction(0x17, PopStackSegment);

        // push ds
        _vm.RegisterInstruction(0x1e, PushDataSegment);

        // pop ds
        _vm.RegisterInstruction(0x1f, PopDataSegment);

        // push r16
        for (byte opcode = 0x50; opcode <= 0x57; opcode++)
        {
            _vm.RegisterInstruction(opcode, PushWordRegister);
        }

        // pop r16
        for (byte opcode = 0x58; opcode <= 0x5f; opcode++)
        {
            _vm.RegisterInstruction(opcode, PopWordRegister);
        }

        // pusha
        _vm.RegisterInstruction(0x60, PushAll);

        // popa
        _vm.RegisterInstruction(0x61, PopAll);

        // push imm16
        _vm.RegisterInstruction(0x68, PushImmediateWord);

        // push imm8
        _vm.RegisterInstruction(0x6a, PushImmediateByte);

        // pop r/m16
        _vm.RegisterInstruction(0x8f, PopRegMemWord);

        // pushf
        _vm.RegisterInstruction(0x9c, PushFlags);

        // popf
        _vm.RegisterInstruction(0x9d, PopFlags);

        // leave
        _vm.RegisterInstruction(0xc9, Leave);
    }

    public void PushExtraSegment(byte opcode)
    {
        PushWord(_vm.Registers.ExtraSegment.Value);
    }

    public void PopExtraSegment(byte opcode)
    {
        _vm.Registers.ExtraSegment.Value = PopWord();
    }

    public void PushCodeSegment(byte opcode)
    {
        PushWord(_vm.Registers.CodeSegment.Value);
    }

    public void PopCodeSegment(byte opcode)
    {
        _vm.Registers.CodeSegment.Value = PopWord();
    }

    public void PushStackSegment(byte opcode)
    {
        PushWord(_vm.Registers.StackSegment.Value);
    }

    public void PopStackSegment(byte opcode)
    {
        _vm.Registers.StackSegment.Value = PopWord();
    }

    public void PushDataSegment(byte opcode)
    {
        PushWord(_vm.Registers.DataSegment.Value);
    }

    public void PopDataSegment(byte opcode)
    {
        _vm.Registers.DataSegment.Value = PopWord();
    }

    public void PushWordRegister(byte opcode)
    {
        PushWord(GetWordRegisterFromOpcode(opcode).Value);
    }
    
    public void PopWordRegister(byte opcode)
    {
        GetWordRegisterFromOpcode(opcode).Value = PopWord();
    }

    public void PushAll(byte opcode)
    {
        var stackPointer = _vm.Registers.StackPointer.Value;
        PushWord(_vm.Registers.GeneralA.Value);
        PushWord(_vm.Registers.GeneralC.Value);
        PushWord(_vm.Registers.GeneralD.Value);
        PushWord(_vm.Registers.GeneralB.Value);
        PushWord(stackPointer);
        PushWord(_vm.Registers.BasePointer.Value);
        PushWord(_vm.Registers.SourceIndex.Value);
        PushWord(_vm.Registers.DestinationIndex.Value);
    }

    public void PopAll(byte opcode)
    {
        _vm.Registers.DestinationIndex.Value = PopWord();
        _vm.Registers.SourceIndex.Value = PopWord();
        _vm.Registers.BasePointer.Value = PopWord();
        PopWord(); // Discard popped stack pointer value
        _vm.Registers.GeneralB.Value = PopWord();
        _vm.Registers.GeneralD.Value = PopWord();
        _vm.Registers.GeneralC.Value = PopWord();
        _vm.Registers.GeneralA.Value = PopWord();
    }

    public void PushImmediateWord(byte opcode)
    {
        PushWord(_vm.NextWord());
    }

    public void PushImmediateByte(byte opcode)
    {
        PushByte(_vm.NextByte());
    }

    public void PopRegMemWord(byte opcode)
    {
        var modRm = new ModRm(_vm.NextByte());

        if (modRm.Reg == 0b000)
        {
            if (modRm.Mod == 0b11)
            {
                // Register addressing mode

                _vm.Registers.GetWordRegister(modRm.Rm).Value = PopWord();
            }
            else
            {
                _vm.Memory.SetWord(GetModRmAddress(modRm), PopWord());
            }
        }
        else
        {
           throw new InvalidInstructionException((ushort)((opcode << 8) | modRm.Value));
        }
    }

    public void PushFlags(byte opcode)
    {
        PushWord(_vm.Registers.Flags.Value);
    }

    public void PopFlags(byte opcode)
    {
        _vm.Registers.Flags.SafeSetValue(PopWord());
    }

    public void Leave(byte opcode)
    {
        _vm.Registers.StackPointer.Value = _vm.Registers.BasePointer.Value;
        _vm.Registers.BasePointer.Value = PopWord();
    }

    public void PushWord(IWordAccessor a)
    {
        PushWord(a.Value);
    }
}
