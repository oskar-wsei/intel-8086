namespace Emulator.Core.Instructions;

public class MiscInstructions : InstructionHandler
{
    public MiscInstructions(VirtualMachine vm) : base(vm) { }

    public override void RegisterInstructions()
    {
        // nop
        _vm.RegisterInstruction(0x90, NoOperation);

        // salc
        _vm.RegisterInstruction(0xd6, SetAccumulatorLowOnCarry);

        // hlt
        _vm.RegisterInstruction(0xf4, Halt);

        // cmc
        _vm.RegisterInstruction(0xf5, CarryComplement);

        // clc
        _vm.RegisterInstruction(0xf8, ClearCarry);

        // stc
        _vm.RegisterInstruction(0xf9, SetCarry);

        // cld
        _vm.RegisterInstruction(0xfc, ClearDirection);

        // std
        _vm.RegisterInstruction(0xfd, SetDirection);
    }

    public void NoOperation(byte opcode)
    {
        ;
    }

    public void SetAccumulatorLowOnCarry(byte opcode)
    {
        _vm.Registers.GeneralA.Low.Value = (byte)(_vm.Registers.Flags.Carry ? 0xff : 0x00);
    }

    public void Halt(byte opcode)
    {
        _vm.Halt();
    }

    public void CarryComplement(byte opcode)
    {
        _vm.Registers.Flags.Carry = !_vm.Registers.Flags.Carry;
    }

    public void ClearCarry(byte opcode)
    {
        _vm.Registers.Flags.ClearCarry();
    }

    public void SetCarry(byte opcode)
    {
        _vm.Registers.Flags.SetCarry();
    }

    public void ClearDirection(byte opcode)
    {
        _vm.Registers.Flags.ClearDirection();
    }

    public void SetDirection(byte opcode)
    {
        _vm.Registers.Flags.SetDirection();
    }
}
