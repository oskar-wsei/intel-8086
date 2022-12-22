using System;
using Emulator.Core.Accessors;

namespace Emulator.Core.Instructions;

public class LogicInstructions : InstructionHandler
{
    public LogicInstructions(VirtualMachine vm) : base(vm) { }

    public override void RegisterInstructions()
    {
        // or r/m8, r8
        // or r/m16, r16
        // or r8, r/m8
        // or r16, r/m16
        RegisterModRmInstructions(0x08, Or, Or);

        // or al, imm8
        // or ax, imm16
        RegisterAccumulatorImmediateInstructions(0x0c, Or, Or);

        // and r/m8, r8
        // and r/m16, r16
        // and r8, r/m8
        // and r16, r/m16
        RegisterModRmInstructions(0x20, And, And);

        // and al, imm8
        // and ax, imm16
        RegisterAccumulatorImmediateInstructions(0x24, And, And);

        // xor r/m8, r8
        // xor r/m16, r16
        // xor r8, r/m8
        // xor r16, r/m16
        RegisterModRmInstructions(0x30, Xor, Xor);

        // xor al, imm8
        // xor ax, imm16
        RegisterAccumulatorImmediateInstructions(0x34, Xor, Xor);

        // test r/m8, r8
        _vm.RegisterInstruction(0x84, HandleModRmByteInstruction(Test));

        // test r/m16, r16
        _vm.RegisterInstruction(0x85, HandleModRmWordInstruction(Test));

        // test al, imm8
        _vm.RegisterInstruction(0xa8, TestAccumulatorLowImmediate);

        // test ax, imm16
        _vm.RegisterInstruction(0xa9, TestAccumulatorImmediate);
    }

    public void Or(IByteAccessor dst, IByteAccessor src)
    {
        dst.Value = (byte)Or(dst.Value, src.Value);
    }

    public void Or(IWordAccessor dst, IWordAccessor src)
    {
        dst.Value = Or(dst.Value, src.Value);
    }

    private ushort Or(ushort a, ushort b)
    {
        return PerformLogicOperation(a, b, (a, b) => a | b);
    }

    public void And(IByteAccessor dst, IByteAccessor src)
    {
        dst.Value = (byte)And(dst.Value, src.Value);
    }

    public void And(IWordAccessor dst, IWordAccessor src)
    {
        dst.Value = And(dst.Value, src.Value);
    }

    private ushort And(ushort a, ushort b)
    {
        return PerformLogicOperation(a, b, (a, b) => a & b);
    }

    public void Xor(IByteAccessor dst, IByteAccessor src)
    {
        dst.Value = (byte)Xor(dst.Value, src.Value);
    }

    public void Xor(IWordAccessor dst, IWordAccessor src)
    {
        dst.Value = Xor(dst.Value, src.Value);
    }

    private ushort Xor(ushort a, ushort b)
    {
        return PerformLogicOperation(a, b, (a, b) => a ^ b);
    }

    public void Test(IByteAccessor dst, IByteAccessor src)
    {
        And(dst.Value, src.Value);
    }

    public void Test(IWordAccessor dst, IWordAccessor src)
    {
        And(dst.Value, src.Value);
    }

    public void TestAccumulatorLowImmediate(byte opcode)
    {
        Test(_vm.Registers.GeneralA.Low, new ImmediateByte(_vm.NextByte()));
    }

    public void TestAccumulatorImmediate(byte opcode)
    {
        Test(_vm.Registers.GeneralA, new ImmediateWord(_vm.NextWord()));
    }

    public void Not(IByteAccessor dst)
    {
        dst.Value = (byte)Not(dst.Value);
    }

    public void Not(IWordAccessor dst)
    {
        dst.Value = Not(dst.Value);
    }

    private ushort Not(ushort value)
    {
        return (ushort)~value;
    }

    private ushort PerformLogicOperation(ushort a, ushort b, Func<ushort, ushort, int> func)
    {
        var result = (ushort)func(a, b);
        _vm.CheckValueFlags(result);
        ClearCarryAndOverflow();
        return result;
    }

    private ushort PerformLogicOperation(ushort a, Func<ushort, int> func)
    {
        var result = (ushort)func(a);
        _vm.CheckValueFlags(result);
        ClearCarryAndOverflow();
        return result;
    }

    private void ClearCarryAndOverflow()
    {
        _vm.Registers.Flags.ClearCarry();
        _vm.Registers.Flags.ClearOverflow();
    }
}
