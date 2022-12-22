namespace Emulator.Core.Instructions;

public class InstructionRegistry
{
    protected VirtualMachine _vm;

    public InstructionRegistry(VirtualMachine vm)
    {
        _vm = vm;
    }

    public void RegisterInstructions()
    {
        var handlers = new InstructionHandler[]
        {
            new ArithmeticInstructions(_vm),
            new LogicInstructions(_vm),
            new StackInstructions(_vm),
            new FlowControlInstructions(_vm),
            new MemoryManipulationInstructions(_vm),
            new RegFieldOpcodeExtendedInstructions(_vm),
            new MiscInstructions(_vm),
        };

        foreach (var handler in handlers)
        {
            handler.RegisterInstructions();
        }
    }
}
