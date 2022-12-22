using Emulator.Core.Components.Registers;

namespace EmulatorTests.ComponentTests;

[TestClass]
public class RegisterTests
{
    [TestMethod("Should access high and low byte")]
    public void TestHighAndLowByte()
    {
        var register = new WordRegisterComponent
        {
            Value = 0xbeef
        };

        Assert.AreEqual(register.High.Value, 0xbe);
        Assert.AreEqual(register.Low.Value, 0xef);
    }
}
