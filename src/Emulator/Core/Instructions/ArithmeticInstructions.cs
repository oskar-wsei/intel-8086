using System.Reflection.Emit;
using System.Reflection.Metadata;
using Emulator.Core.Accessors;
using Emulator.Core.Misc;
using Emulator.Core.Utils;

namespace Emulator.Core.Instructions;

public class ArithmeticInstructions : InstructionHandler
{
    public ArithmeticInstructions(VirtualMachine vm) : base(vm) { }

    public override void RegisterInstructions()
    {
        // add r/m8, r8
        // add r/m16, r16
        // add r8, r/m8
        // add r16, r/m16
        RegisterModRmInstructions(0x00, Add, Add);

        // add al, imm8
        // add ax, imm16
        RegisterAccumulatorImmediateInstructions(0x04, Add, Add);

        // adc r/m8, r8
        // adc r/m16, r16
        // adc r8, r/m8
        // adc r16, r/m16
        RegisterModRmInstructions(0x10, AddWithCarry, AddWithCarry);

        // adc al, imm8
        // adc ax, imm16
        RegisterAccumulatorImmediateInstructions(0x14, AddWithCarry, AddWithCarry);

        // sbb r/m8, r8
        // sbb r/m16, r16
        // sbb r8, r/m8
        // sbb r16, r/m16
        RegisterModRmInstructions(0x18, SubtractWithBorrow, SubtractWithBorrow);

        // sbb al, imm8
        // sbb ax, imm16
        RegisterAccumulatorImmediateInstructions(0x1c, SubtractWithBorrow, SubtractWithBorrow);

        // sub r/m8, r8
        // sub r/m16, r16
        // sub r8, r/m8
        // sub r16, r/m16
        RegisterModRmInstructions(0x28, Subtract, Subtract);

        // sub al, imm8
        // sub ax, imm16
        RegisterAccumulatorImmediateInstructions(0x2c, Subtract, Subtract);

        // cmp r/m8, r8
        // cmp r/m16, r16
        // cmp r8, r/m8
        // cmp r16, r/m16
        RegisterModRmInstructions(0x38, Compare, Compare);

        // cmp al, imm8
        // cmp ax, imm16
        RegisterAccumulatorImmediateInstructions(0x3c, Compare, Compare);

        // inc r16
        for (byte opcode = 0x40; opcode <= 0x47; opcode++)
        {
            _vm.RegisterInstruction(opcode, IncrementWordRegister);
        }

        // dec r16
        for (byte opcode = 0x48; opcode <= 0x4f; opcode++)
        {
            _vm.RegisterInstruction(opcode, DecrementWordRegister);
        }

        // imul r16, r/m16, imm16
        _vm.RegisterInstruction(0x69, SignedMultiplyRegisterImmediateWord);

        // imul r16, r/m16, imm8
        _vm.RegisterInstruction(0x6b, SignedMultiplyRegisterImmediateByte);

        // cmpsb
        _vm.RegisterInstruction(0xa6, CompareStringByte);

        // cmpsw
        _vm.RegisterInstruction(0xa7, CompareStringWord);

        // scasb
        _vm.RegisterInstruction(0xae, ScanStringByte);

        // scasw
        _vm.RegisterInstruction(0xaf, ScanStringWord);
    }

    public void Add(IByteAccessor dst, IByteAccessor src)
    {
        _vm.Registers.Flags.SetCarry(
            (dst.Value > byte.MaxValue - src.Value) | (src.Value > byte.MaxValue - dst.Value)
        );

        var result = (byte)(dst.Value + src.Value);

        _vm.CheckValueFlags(result);

        // True if:
        // (-) + (-) = (+)
        // (+) + (+) = (-)
        _vm.Registers.Flags.SetOverflow(((~(dst.Value ^ src.Value)) & (dst.Value ^ result) & 0x80) != 0);

        dst.Value = result;
    }

    public void Add(IWordAccessor dst, IWordAccessor src)
    {
        _vm.Registers.Flags.SetCarry(
            (dst.Value > ushort.MaxValue - src.Value) | (src.Value > ushort.MaxValue - dst.Value)
        );

        var result = (ushort)(dst.Value + src.Value);

        _vm.CheckValueFlags(dst.Value);

        // True if:
        // (-) + (-) = (+)
        // (+) + (+) = (-)
        _vm.Registers.Flags.SetOverflow(((~(dst.Value ^ src.Value)) & (dst.Value ^ result) & 0x8000) != 0);

        dst.Value = result;
    }

    public void AddWithCarry(IByteAccessor dst, IByteAccessor src)
    {
        var shouldAddCarry = _vm.Registers.Flags.Carry;

        Add(dst, src);

        if (shouldAddCarry)
        {
            var hasCarried = _vm.Registers.Flags.Carry;
            var hasOverflown = _vm.Registers.Flags.Overflow;

            Add(dst, new ImmediateByte(1));

            _vm.Registers.Flags.Carry = _vm.Registers.Flags.Carry || hasCarried;
            _vm.Registers.Flags.Overflow = _vm.Registers.Flags.Overflow || hasOverflown;
        }
    }

    public void AddWithCarry(IWordAccessor dst, IWordAccessor src)
    {
        var shouldAddCarry = _vm.Registers.Flags.Carry;

        Add(dst, src);

        if (shouldAddCarry)
        {
            var hasCarried = _vm.Registers.Flags.Carry;
            var hasOverflown = _vm.Registers.Flags.Overflow;

            Add(dst, new ImmediateWord(1));

            _vm.Registers.Flags.Carry = _vm.Registers.Flags.Carry || hasCarried;
            _vm.Registers.Flags.Overflow = _vm.Registers.Flags.Overflow || hasOverflown;
        }
    }

    public void Subtract(IByteAccessor dst, IByteAccessor src)
    {
        _vm.Registers.Flags.SetCarry(dst.Value < src.Value);

        var result = (byte)(dst.Value - src.Value);

        _vm.CheckValueFlags(result);

        // True if:
        // (-) - (+) = (+)
        // (+) - (-) = (-)
        _vm.Registers.Flags.SetOverflow(((~(src.Value ^ result)) & (dst.Value ^ result) & 0x80) != 0);

        dst.Value = result;
    }

    public void Subtract(IWordAccessor dst, IWordAccessor src)
    {
        _vm.Registers.Flags.SetCarry(dst.Value < src.Value);

        var result = (ushort)(dst.Value - src.Value);

        _vm.CheckValueFlags(result);

        // True if:
        // (-) - (+) = (+)
        // (+) - (-) = (-)
        _vm.Registers.Flags.SetOverflow(((~(src.Value ^ result)) & (dst.Value ^ result) & 0x8000) != 0);

        dst.Value = result;
    }

    public void SubtractWithBorrow(IByteAccessor dst, IByteAccessor src)
    {
        var shouldSubtractBorrow = _vm.Registers.Flags.Carry;

        Subtract(dst, src);

        if (shouldSubtractBorrow)
        {
            var hasBorrowed = _vm.Registers.Flags.Carry;
            var hasOverflown = _vm.Registers.Flags.Overflow;

            Subtract(dst, new ImmediateByte(1));

            _vm.Registers.Flags.Carry = _vm.Registers.Flags.Carry || hasBorrowed;
            _vm.Registers.Flags.Overflow = _vm.Registers.Flags.Overflow || hasOverflown;
        }
    }

    public void SubtractWithBorrow(IWordAccessor dst, IWordAccessor src)
    {
        var shouldSubtractBorrow = _vm.Registers.Flags.Carry;

        Subtract(dst, src);

        if (shouldSubtractBorrow)
        {
            var hasBorrowed = _vm.Registers.Flags.Carry;
            var hasOverflown = _vm.Registers.Flags.Overflow;

            Subtract(dst, new ImmediateWord(1));

            _vm.Registers.Flags.Carry = _vm.Registers.Flags.Carry || hasBorrowed;
            _vm.Registers.Flags.Overflow = _vm.Registers.Flags.Overflow || hasOverflown;
        }
    }

    public void Compare(IByteAccessor dst, IByteAccessor src)
    {
        var preservedValue = dst.Value;
        Subtract(dst, src);
        dst.Value = preservedValue;
    }

    public void Compare(IWordAccessor dst, IWordAccessor src)
    {
        var preservedValue = dst.Value;
        Subtract(dst, src);
        dst.Value = preservedValue;
    }

    public void Increment(IByteAccessor dst)
    {
        var preservedCarry = _vm.Registers.Flags.Carry;
        Add(dst, new ImmediateByte(1));
        _vm.Registers.Flags.Carry = preservedCarry;
    }

    public void Increment(IWordAccessor dst)
    {
        var preservedCarry = _vm.Registers.Flags.Carry;
        Add(dst, new ImmediateWord(1));
        _vm.Registers.Flags.Carry = preservedCarry;
    }

    public void Decrement(IByteAccessor dst)
    {
        var preservedCarry = _vm.Registers.Flags.Carry;
        Subtract(dst, new ImmediateByte(1));
        _vm.Registers.Flags.Carry = preservedCarry;
    }

    public void Decrement(IWordAccessor dst)
    {
        var preservedCarry = _vm.Registers.Flags.Carry;
        Subtract(dst, new ImmediateWord(1));
        _vm.Registers.Flags.Carry = preservedCarry;
    }

    public void IncrementWordRegister(byte opcode)
    {
        Increment(GetWordRegisterFromOpcode(opcode));
    }

    public void DecrementWordRegister(byte opcode)
    {
        Decrement(GetWordRegisterFromOpcode(opcode));
    }

    public void CompareStringByte(byte opcode)
    {
        var a = new ImmediateByte(_vm.Memory.GetByte(_vm.Registers.SourceIndex.Value));
        var b = new ImmediateByte(_vm.Memory.GetByte(_vm.Registers.DestinationIndex.Value));
        Subtract(a, b);
    }

    public void CompareStringWord(byte opcode)
    {
        var a = new ImmediateWord(_vm.Memory.GetWord(_vm.Registers.SourceIndex.Value));
        var b = new ImmediateWord(_vm.Memory.GetWord(_vm.Registers.DestinationIndex.Value));
        Subtract(a, b);
    }

    public void ScanStringByte(byte opcode)
    {
        var a = new ImmediateByte(_vm.Registers.GeneralA.Low.Value);
        var b = new ImmediateByte(_vm.Memory.GetByte(_vm.Registers.DestinationIndex.Value));
        Subtract(a, b);
        _vm.Registers.DestinationIndex.Value += (ushort)(_vm.Registers.Flags.Direction ? -1 : 1);
    }

    public void ScanStringWord(byte opcode)
    {
        var a = new ImmediateWord(_vm.Registers.GeneralA.Value);
        var b = new ImmediateWord(_vm.Memory.GetWord(_vm.Registers.DestinationIndex.Value));
        Subtract(a, b);
        _vm.Registers.DestinationIndex.Value += (ushort)(_vm.Registers.Flags.Direction ? -2 : 2);
    }

    public void Negate(IByteAccessor dst)
    {
        _vm.Registers.Flags.SetCarry(dst.Value != 0);
        dst.Value = ArithmeticUtils.Negate(dst.Value);
        _vm.CheckValueFlags(dst.Value);
    }

    public void Negate(IWordAccessor dst)
    {
        _vm.Registers.Flags.SetCarry(dst.Value != 0);
        dst.Value = ArithmeticUtils.Negate(dst.Value);
        _vm.CheckValueFlags(dst.Value);
    }

    public void UnsignedMultiplyAccumulator(IByteAccessor src)
    {
        var result = (ushort)(_vm.Registers.GeneralA.Low.Value * src.Value);
        var resultHigh = (byte)((result & 0xff00) >> 8);

        SetCarryAndOverflow(resultHigh != 0);

        _vm.Registers.GeneralA.Value = result;
    }

    public void UnsignedMultiplyAccumulator(IWordAccessor src)
    {
        var result = (uint)(_vm.Registers.GeneralA.Value * src.Value);
        var resultHigh = (ushort)((result & 0xffff0000) >> 16);
        var resultLow = (ushort)(result & 0xffff);

        SetCarryAndOverflow(resultHigh != 0);

        _vm.Registers.GeneralD.Value = resultHigh;
        _vm.Registers.GeneralA.Value = resultLow;
    }

    public void SignedMultiplyRegisterImmediateWord(byte opcode)
    {
        var modRm = new ModRm(_vm.NextByte());
        var (operandA, dst) = GetModRmWordOperands(opcode, modRm);
        var operandB = new ImmediateWord(_vm.NextWord());
        var (_, resultLow) = SignedMultiply(operandA, operandB);
        dst.Value = resultLow;
    }

    public void SignedMultiplyRegisterImmediateByte(byte opcode)
    {
        var modRm = new ModRm(_vm.NextByte());
        var (dst, operandA) = GetModRmWordOperands(opcode, modRm);
        var operandB = new ImmediateWord(ArithmeticUtils.SignExtend(_vm.NextByte()));
        var (_, resultLow) = SignedMultiply(operandA, operandB);
        dst.Value = resultLow;
    }

    public ushort SignedMultiply(IByteAccessor operandA, IByteAccessor operandB)
    {
        var a = ArithmeticUtils.SignExtend(operandA.Value);
        var b = ArithmeticUtils.SignExtend(operandB.Value);
        var result = (ushort)(a * b);
        var resultHigh = (byte)((result & 0xff00) >> 8);

        SetCarryAndOverflow(resultHigh != 0 && resultHigh != 0xff);

        return result;
    }

    public (ushort, ushort) SignedMultiply(IWordAccessor operandA, IWordAccessor operandB)
    {
        var a = ArithmeticUtils.SignExtend(operandA.Value);
        var b = ArithmeticUtils.SignExtend(operandB.Value);
        var result = a * b;
        var resultHigh = (ushort)((result & 0xffff0000) >> 16);
        var resultLow = (ushort)(result & 0xffff);

        SetCarryAndOverflow(resultHigh != 0 && resultHigh != 0xff);

        return (resultHigh, resultLow);
    }

    public void SignedMultiplyAccumulator(IByteAccessor src)
    {
        _vm.Registers.GeneralA.Value = SignedMultiply(_vm.Registers.GeneralA.Low, src);
    }

    public void SignedMultiplyAccumulator(IWordAccessor src)
    {
        var (resultHigh, resultLow) = SignedMultiply(_vm.Registers.GeneralA, src);
        _vm.Registers.GeneralD.Value = resultHigh;
        _vm.Registers.GeneralA.Value = resultLow;
    }

    public void UnsignedDivideAccumulator(IByteAccessor src)
    {
        var a = _vm.Registers.GeneralA.Value;
        var quotient = (byte)(a / src.Value);
        var remainder = (byte)(a % src.Value);

        _vm.Registers.GeneralA.Low.Value = quotient;
        _vm.Registers.GeneralA.High.Value = remainder;
    }

    public void UnsignedDivideAccumulator(IWordAccessor src)
    {
        var a = (uint)((_vm.Registers.GeneralD.Value << 16) | _vm.Registers.GeneralA.Value);
        var quotient = (ushort)(a / src.Value);
        var remainder = (ushort)(a % src.Value);

        _vm.Registers.GeneralA.Value = quotient;
        _vm.Registers.GeneralD.Value = remainder;
    }

    public void SignedDivideAccumulator(IByteAccessor src)
    {
        var a = (short)_vm.Registers.GeneralA.Value;
        var b = (short)ArithmeticUtils.SignExtend(src.Value);

        var quotient = (byte)(a / b);
        var remainder = (byte)(a % b);

        _vm.Registers.GeneralA.Low.Value = quotient;
        _vm.Registers.GeneralA.High.Value = remainder;
    }

    public void SignedDivideAccumulator(IWordAccessor src)
    {
        var a = (_vm.Registers.GeneralD.Value << 16) | _vm.Registers.GeneralA.Value;
        var b = (int)ArithmeticUtils.SignExtend(src.Value);
        var quotient = (ushort)(a / b);
        var remainder = (ushort)(a % b);

        _vm.Registers.GeneralA.Value = quotient;
        _vm.Registers.GeneralD.Value = remainder;
    }

    private void SetCarryAndOverflow(bool value = true)
    {
        _vm.Registers.Flags.SetCarry(value);
        _vm.Registers.Flags.SetOverflow(value);
    }
}
