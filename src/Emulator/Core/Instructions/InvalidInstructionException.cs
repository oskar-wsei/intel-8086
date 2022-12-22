using Emulator.Core.Utils;

namespace Emulator.Core.Instructions;

internal class InvalidInstructionException : EmulationException
{
    public InvalidInstructionException(byte opcode)
        : base($"Invalid instruction: {FormatUtils.ToHex(opcode)}") { }

    public InvalidInstructionException(byte opcode, byte extension)
        : this((ushort)((opcode << 8) | extension)) { }

    public InvalidInstructionException(ushort opcode)
        : base($"Invalid instruction: {FormatUtils.ToHex(opcode)}") { }
}
