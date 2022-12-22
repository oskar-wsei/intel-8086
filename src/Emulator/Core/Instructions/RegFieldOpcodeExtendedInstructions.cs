using Emulator.Core.Accessors;
using Emulator.Core.Misc;
using Emulator.Core.Utils;
using System;

namespace Emulator.Core.Instructions;

public class RegFieldOpcodeExtendedInstructions : InstructionHandler
{
    private readonly ArithmeticInstructions _arithmetic;
    private readonly LogicInstructions _logic;
    private readonly MemoryManipulationInstructions _memoryManipulation;
    private readonly FlowControlInstructions _flowControl;
    private readonly StackInstructions _stack;

    public RegFieldOpcodeExtendedInstructions(VirtualMachine vm) : base(vm)
    {
        _arithmetic = new ArithmeticInstructions(vm);
        _logic = new LogicInstructions(vm);
        _memoryManipulation = new MemoryManipulationInstructions(vm);
        _flowControl = new FlowControlInstructions(vm);
        _stack = new StackInstructions(vm);
    }

    public override void RegisterInstructions()
    {
        _vm.RegisterInstruction(0x80, PerformImmediateModeInstruction);
        _vm.RegisterInstruction(0x81, PerformImmediateModeInstruction);
        _vm.RegisterInstruction(0x82, PerformImmediateModeInstruction);
        _vm.RegisterInstruction(0x83, PerformImmediateModeInstruction);

        _vm.RegisterInstruction(0xc6, PerformImmediateModeInstruction2);
        _vm.RegisterInstruction(0xc7, PerformImmediateModeInstruction2);

        _vm.RegisterInstruction(0xf6, PerformInstructionF6F7);
        _vm.RegisterInstruction(0xf7, PerformInstructionF6F7);

        _vm.RegisterInstruction(0xfe, PerformInstructionFE);
        _vm.RegisterInstruction(0xff, PerformInstructionFF);
    }

    public void PerformImmediateModeInstruction(byte opcode)
    {
        var modRm = new ModRm(_vm.NextByte());

        switch (modRm.Reg)
        {
            case 0b000:
                // add r/m8, imm8
                // add r/m16, imm8
                // add r/m16, imm16
                PerformImmediateModeInstruction(opcode, modRm, _arithmetic.Add, _arithmetic.Add);
                break;
            case 0b001:
                // or r/m8, imm8
                // or r/m16, imm8
                // or r/m16, imm16
                PerformImmediateModeInstruction(opcode, modRm, _logic.Or, _logic.Or);
                break;
            case 0b010:
                // adc r/m8, imm8
                // adc r/m16, imm8
                // adc r/m16, imm16
                PerformImmediateModeInstruction(opcode, modRm, _arithmetic.AddWithCarry, _arithmetic.AddWithCarry);
                break;
            case 0b011:
                // sbb r/m8, imm8
                // sbb r/m16, imm8
                // sbb r/m16, imm16
                PerformImmediateModeInstruction(opcode, modRm, _arithmetic.SubtractWithBorrow, _arithmetic.SubtractWithBorrow);
                break;
            case 0b100:
                // and r/m8, imm8
                // and r/m16, imm8
                // and r/m16, imm16
                PerformImmediateModeInstruction(opcode, modRm, _logic.And, _logic.And);
                break;
            case 0b101:
                // sub r/m8, imm8
                // sub r/m16, imm8
                // sub r/m16, imm16
                PerformImmediateModeInstruction(opcode, modRm, _arithmetic.Subtract, _arithmetic.Subtract);
                break;
            case 0b110:
                // xor r/m8, imm8
                // xor r/m16, imm8
                // xor r/m16, imm16
                PerformImmediateModeInstruction(opcode, modRm, _logic.Xor, _logic.Xor);
                break;
            case 0b111:
                // cmp r/m8, imm8
                // cmp r/m16, imm8
                // cmp r/m16, imm16
                PerformImmediateModeInstruction(opcode, modRm, _arithmetic.Compare, _arithmetic.Compare);
                break;
            default:
                break;
        }
    }

    public void PerformImmediateModeInstruction2(byte opcode)
    {
        var modRm = new ModRm(_vm.NextByte());

        switch (modRm.Reg)
        {
            case 0b000:
                // mov r/m8, imm8
                PerformImmediateModeInstruction(opcode, modRm, _memoryManipulation.Move, _memoryManipulation.Move, true);
                break;
            default:
                throw new InvalidInstructionException(opcode, modRm.Value);
        }
    }

    public void PerformInstructionF6F7(byte opcode)
    {
        var modRm = new ModRm(_vm.NextByte());

        switch (modRm.Reg)
        {
            case 0b000:
                // test r/m8, imm8
                // test r/m16, imm16
                PerformImmediateModeInstruction(opcode, modRm, _logic.Test, _logic.Test);
                break;
            case 0b010:
                // not r/m8
                // not r/m16
                PerformSourceOperandOnlyInstruction(opcode, modRm, _logic.Not, _logic.Not);
                break;
            case 0b011:
                // neg r/m8
                // neg r/m16
                PerformSourceOperandOnlyInstruction(opcode, modRm, _arithmetic.Negate, _arithmetic.Negate);
                break;
            case 0b100:
                // mul r/m8
                // mul r/m16
                PerformSourceOperandOnlyInstruction(opcode, modRm, _arithmetic.UnsignedMultiplyAccumulator, _arithmetic.UnsignedMultiplyAccumulator);
                break;
            case 0b101:
                // imul r/m8
                // imul r/m16
                PerformSourceOperandOnlyInstruction(opcode, modRm, _arithmetic.SignedMultiplyAccumulator, _arithmetic.SignedMultiplyAccumulator);
                break;
            case 0b110:
                // div r/m8
                // div r/m16
                PerformSourceOperandOnlyInstruction(opcode, modRm, _arithmetic.UnsignedDivideAccumulator, _arithmetic.UnsignedDivideAccumulator);
                break;
            case 0b111:
                // idiv r/m8
                // idiv r/m16
                PerformSourceOperandOnlyInstruction(opcode, modRm, _arithmetic.SignedDivideAccumulator, _arithmetic.SignedDivideAccumulator);
                break;
            default:
                throw new InvalidInstructionException(opcode, modRm.Value);
        }
    }

    public void PerformInstructionFE(byte opcode)
    {
        var modRm = new ModRm(_vm.NextByte());

        switch (modRm.Reg)
        {
            case 0b000:
                // inc r/m8
                PerformSourceOperandOnlyByteInstruction(opcode, modRm, _arithmetic.Increment);
                break;
            case 0b001:
                // dec r/m8
                PerformSourceOperandOnlyByteInstruction(opcode, modRm, _arithmetic.Decrement);
                break;
            default:
                throw new InvalidInstructionException(opcode, modRm.Value);
        }
    }

    public void PerformInstructionFF(byte opcode)
    {
        var modRm = new ModRm(_vm.NextByte());

        switch (modRm.Reg)
        {
            case 0b000:
                // inc r/m16
                PerformSourceOperandOnlyWordInstruction(opcode, modRm, _arithmetic.Increment);
                break;
            case 0b001:
                // dec r/m16
                PerformSourceOperandOnlyWordInstruction(opcode, modRm, _arithmetic.Decrement);
                break;
            case 0b010:
                // call r/m16
                PerformSourceOperandOnlyWordInstruction(opcode, modRm, _flowControl.PerformCallNearAbsolute);
                break;
            case 0b100:
                // jmp r/m16
                PerformSourceOperandOnlyWordInstruction(opcode, modRm, _flowControl.PerformJumpNearAbsolute);
                break;
            case 0b110:
                // push r/m16
                PerformSourceOperandOnlyWordInstruction(opcode, modRm, _stack.PushWord);
                break;
            default:
                throw new InvalidInstructionException(opcode, modRm.Value);
        }
    }

    private void PerformImmediateModeInstruction(
        byte opcode,
        ModRm modRm,
        Action<IByteAccessor, IByteAccessor> byteHandler,
        Action<IWordAccessor, IWordAccessor> wordHandler,
        bool ignoreXBit = false
    )
    {
        var isDestinationSizeByte = (opcode & 0b01) == 0;
        var isConstantSizeSameAsDestinationSize = (opcode & 0b10) == 0;

        if (isDestinationSizeByte)
        {
            byteHandler(GetModRmDestinationByteOperand(modRm), new ImmediateByte(_vm.NextByte()));
        }
        else
        {
            if (isConstantSizeSameAsDestinationSize || ignoreXBit)
            {
                wordHandler(GetModRmDestinationWordOperand(modRm), new ImmediateWord(_vm.NextWord()));
            }
            else
            {
                wordHandler(GetModRmDestinationWordOperand(modRm), new ImmediateWord(ArithmeticUtils.SignExtend(_vm.NextByte())));
            }
        }
    }

    private void PerformSourceOperandOnlyInstruction(
        byte opcode,
        ModRm modRm,
        Action<IByteAccessor> byteHandler,
        Action<IWordAccessor> wordHandler
    )
    {
        var isDestinationSizeByte = (opcode & 0b01) == 0;

        if (isDestinationSizeByte)
        {
            byteHandler(GetModRmDestinationByteOperand(modRm));
        }
        else
        {
            wordHandler(GetModRmDestinationWordOperand(modRm));
        }
    }

    private void PerformSourceOperandOnlyByteInstruction(
        byte opcode,
        ModRm modRm,
        Action<IByteAccessor> byteHandler
    )
    {
        byteHandler(GetModRmDestinationByteOperand(modRm));
    }

    private void PerformSourceOperandOnlyWordInstruction(
        byte opcode,
        ModRm modRm,
        Action<IWordAccessor> wordHandler
    )
    {
        wordHandler(GetModRmDestinationWordOperand(modRm));
    }
}
