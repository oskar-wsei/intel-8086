using Emulator.Core.Components.Memory;

namespace EmulatorTests.ComponentTests;

[TestClass]
public class MemoryTests
{
    private readonly uint _size = 64;
#pragma warning disable CS8618
    private MemoryComponent _memory;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
        _memory = new MemoryComponent(_size);
    }

    [TestMethod("When address >= size should throw exception")]
    public void TestOutOfRangeAccess()
    {
        Assert.ThrowsException<MemoryAccessException>(() => _memory.GetByte(_size));
        Assert.ThrowsException<MemoryAccessException>(() => _memory.GetByte(_size + 1));
    }

    [TestMethod("Should store data")]
    public void TestStoreData()
    {
        uint address = 10;
        byte value = 0xaa;

        _memory.SetByte(address, value);

        Assert.AreEqual(value, _memory.GetByte(address));
    }

    [TestMethod("Should store words little endian")]
    public void TestWordEndianness()
    {
        uint address = 10;
        ushort value = 0xbeef;

        _memory.SetWord(address, value);

        Assert.AreEqual(value, _memory.GetWord(address));
        Assert.AreEqual(0xef, _memory.GetByte(address));
        Assert.AreEqual(0xbe, _memory.GetByte(address + 1));
    }
}
