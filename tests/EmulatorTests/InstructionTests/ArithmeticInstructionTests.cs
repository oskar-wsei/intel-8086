using System.Security.Cryptography;
using Emulator.Core;
using Emulator.Core.Accessors;
using Emulator.Core.Instructions;

namespace EmulatorTests.InstructionTests;

[TestClass]
public class ArithmeticInstructionTests
{
#pragma warning disable CS8618
    private VirtualMachine _vm;
    private ArithmeticInstructions _instructions;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
        _vm = new VirtualMachine();
        _instructions = new ArithmeticInstructions(_vm);
    }

    [TestMethod]
    public void TestAddBytes()
    {
        var flags = _vm.Registers.Flags;
        var dst = new ImmediateByte(0x00);
        var src = new ImmediateByte(0x00);

        // 

        dst.Value = 0x7f;
        src.Value = 0x00;

        _instructions.Add(dst, src);

        Assert.AreEqual(0x7f, dst.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsFalse(flags.Sign);
        Assert.IsFalse(flags.Zero);
        Assert.IsFalse(flags.Carry);

        // 

        dst.Value = 0xff;
        src.Value = 0x7f;

        _instructions.Add(dst, src);

        Assert.AreEqual(0x7e, dst.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsFalse(flags.Sign);
        Assert.IsFalse(flags.Zero);
        Assert.IsTrue(flags.Carry);

        //

        dst.Value = 0x00;
        src.Value = 0x00;

        _instructions.Add(dst, src);

        Assert.AreEqual(0x00, dst.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsFalse(flags.Sign);
        Assert.IsTrue(flags.Zero);
        Assert.IsFalse(flags.Carry);

        //

        dst.Value = 0xff;
        src.Value = 0x01;

        _instructions.Add(dst, src);

        Assert.AreEqual(0x00, dst.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsFalse(flags.Sign);
        Assert.IsTrue(flags.Zero);
        Assert.IsTrue(flags.Carry);

        //

        dst.Value = 0xff;
        src.Value = 0x00;

        _instructions.Add(dst, src);

        Assert.AreEqual(0xff, dst.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsTrue(flags.Sign);
        Assert.IsFalse(flags.Zero);
        Assert.IsFalse(flags.Carry);

        //

        dst.Value = 0xff;
        src.Value = 0xff;

        _instructions.Add(dst, src);

        Assert.AreEqual(0xfe, dst.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsTrue(flags.Sign);
        Assert.IsFalse(flags.Zero);
        Assert.IsTrue(flags.Carry);

        //

        dst.Value = 0xff;
        src.Value = 0x80;

        _instructions.Add(dst, src);

        Assert.AreEqual(0x7f, dst.Value);
        Assert.IsTrue(flags.Overflow);
        Assert.IsFalse(flags.Sign);
        Assert.IsFalse(flags.Zero);
        Assert.IsTrue(flags.Carry);

        //

        dst.Value = 0x80;
        src.Value = 0x80;

        _instructions.Add(dst, src);

        Assert.AreEqual(0x00, dst.Value);
        Assert.IsTrue(flags.Overflow);
        Assert.IsFalse(flags.Sign);
        Assert.IsTrue(flags.Zero);
        Assert.IsTrue(flags.Carry);

        //

        dst.Value = 0x7f;
        src.Value = 0x7f;

        _instructions.Add(dst, src);

        Assert.AreEqual(0x0fe, dst.Value);
        Assert.IsTrue(flags.Overflow);
        Assert.IsTrue(flags.Sign);
        Assert.IsFalse(flags.Zero);
        Assert.IsFalse(flags.Carry);
    }

    [TestMethod]
    public void TestSubtractBytes()
    {
        var flags = _vm.Registers.Flags;
        var dst = new ImmediateByte(0x00);
        var src = new ImmediateByte(0x00);

        //

        dst.Value = 0xff;
        src.Value = 0xfe;

        _instructions.Subtract(dst, src);

        Assert.AreEqual(0x1, dst.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsFalse(flags.Sign);
        Assert.IsFalse(flags.Zero);
        Assert.IsFalse(flags.Carry);

        //

        dst.Value = 0x7e;
        src.Value = 0xff;

        _instructions.Subtract(dst, src);

        Assert.AreEqual(0x7f, dst.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsFalse(flags.Sign);
        Assert.IsFalse(flags.Zero);
        Assert.IsTrue(flags.Carry);

        //

        dst.Value = 0xff;
        src.Value = 0xff;

        _instructions.Subtract(dst, src);

        Assert.AreEqual(0x00, dst.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsFalse(flags.Sign);
        Assert.IsTrue(flags.Zero);
        Assert.IsFalse(flags.Carry);

        //

        dst.Value = 0xff;
        src.Value = 0x7f;

        _instructions.Subtract(dst, src);

        Assert.AreEqual(0x80, dst.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsTrue(flags.Sign);
        Assert.IsFalse(flags.Zero);
        Assert.IsFalse(flags.Carry);

        //

        dst.Value = 0xfe;
        src.Value = 0xff;

        _instructions.Subtract(dst, src);

        Assert.AreEqual(0xff, dst.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsTrue(flags.Sign);
        Assert.IsFalse(flags.Zero);
        Assert.IsTrue(flags.Carry);

        //

        dst.Value = 0xfe;
        src.Value = 0x7f;

        _instructions.Subtract(dst, src);

        Assert.AreEqual(0x7f, dst.Value);
        Assert.IsTrue(flags.Overflow);
        Assert.IsFalse(flags.Sign);
        Assert.IsFalse(flags.Zero);
        Assert.IsFalse(flags.Carry);

        //

        dst.Value = 0x7f;
        src.Value = 0xff;

        _instructions.Subtract(dst, src);

        Assert.AreEqual(0x80, dst.Value);
        Assert.IsTrue(flags.Overflow);
        Assert.IsTrue(flags.Sign);
        Assert.IsFalse(flags.Zero);
        Assert.IsTrue(flags.Carry);
    }

    [TestMethod]
    public void TestAddWithCarry()
    {
        var flags = _vm.Registers.Flags;
        flags.SetCarry();

        var dst = new ImmediateByte(80);
        var src = new ImmediateByte(60);

        _instructions.AddWithCarry(dst, src);

        Assert.AreEqual(141, dst.Value);
        Assert.IsTrue(flags.Overflow);
        Assert.IsTrue(flags.Sign);
        Assert.IsFalse(flags.Carry);

        dst.Value = 127;
        src.Value = 0;

        flags.SetCarry();

        _instructions.AddWithCarry(dst, src);

        Assert.AreEqual(128, dst.Value);
        Assert.IsTrue(flags.Overflow);
        Assert.IsTrue(flags.Sign);
        Assert.IsFalse(flags.Carry);

        dst.Value = 255;
        src.Value = 255;

        flags.SetCarry();

        _instructions.AddWithCarry(dst, src);

        Assert.AreEqual(255, dst.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsTrue(flags.Sign);
        Assert.IsTrue(flags.Carry);
    }

    [TestMethod]
    public void TestSubtractWithBorrow()
    {
        var flags = _vm.Registers.Flags;
        flags.SetCarry();

        var dst = new ImmediateByte(80);
        var src = new ImmediateByte(60);

        _instructions.SubtractWithBorrow(dst, src);

        Assert.AreEqual(19, dst.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsFalse(flags.Sign);
        Assert.IsFalse(flags.Carry);

        dst.Value = 128;
        src.Value = 0;

        flags.SetCarry();

        _instructions.SubtractWithBorrow(dst, src);

        Assert.AreEqual(127, dst.Value);
        Assert.IsTrue(flags.Overflow);
        Assert.IsFalse(flags.Sign);
        Assert.IsFalse(flags.Carry);
    }

    [TestMethod]
    public void TestUnsignedMultiply()
    {
        var flags = _vm.Registers.Flags;

        _vm.Registers.GeneralA.Low.Value = 8;

        var src = new ImmediateByte(12);

        _instructions.UnsignedMultiplyAccumulator(src);

        Assert.AreEqual(96, _vm.Registers.GeneralA.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsFalse(flags.Carry);
    }

    [TestMethod]
    public void TestUnsignedMultiplyWord()
    {
        var flags = _vm.Registers.Flags;

        _vm.Registers.GeneralA.Value = 1896;

        var src = new ImmediateWord(1458);

        _instructions.UnsignedMultiplyAccumulator(src);

        Assert.AreEqual(0x2a, _vm.Registers.GeneralD.Value);
        Assert.AreEqual(0x2e50, _vm.Registers.GeneralA.Value);
        Assert.IsTrue(flags.Overflow);
        Assert.IsTrue(flags.Carry);
    }

    [TestMethod]
    public void TestSignedMultiply()
    {
        var flags = _vm.Registers.Flags;

        _vm.Registers.GeneralA.Low.Value = 0xfe; // -2

        var src = new ImmediateByte(12);

        _instructions.SignedMultiplyAccumulator(src);

        // -24
        Assert.AreEqual(0xffe8, _vm.Registers.GeneralA.Value);
        Assert.IsFalse(flags.Overflow);
        Assert.IsFalse(flags.Carry);
    }

    [TestMethod]
    public void TestSignedMultiplyWord()
    {
        var flags = _vm.Registers.Flags;

        _vm.Registers.GeneralA.Value = 0xfbff; // -1025

        var src = new ImmediateWord(163);

        _instructions.SignedMultiplyAccumulator(src);

        // -167075
        Assert.AreEqual(0xfffd, _vm.Registers.GeneralD.Value);
        Assert.AreEqual(0x735d, _vm.Registers.GeneralA.Value);
        Assert.IsTrue(flags.Overflow);
        Assert.IsTrue(flags.Carry);
    }

    [TestMethod]
    public void TestUnsignedDivide()
    {
        _vm.Registers.GeneralA.Value = 25;

        var src = new ImmediateByte(2);

        _instructions.UnsignedDivideAccumulator(src);

        Assert.AreEqual(12, _vm.Registers.GeneralA.Low.Value);
        Assert.AreEqual(1, _vm.Registers.GeneralA.High.Value);
    }

    [TestMethod]
    public void TestSignedDivide()
    {
        _vm.Registers.GeneralA.Value = 25;

        var src = new ImmediateByte(0xfe); // -2

        _instructions.SignedDivideAccumulator(src);

        // -12
        Assert.AreEqual(0xf4, _vm.Registers.GeneralA.Low.Value);
        Assert.AreEqual(1, _vm.Registers.GeneralA.High.Value);
    }
}
