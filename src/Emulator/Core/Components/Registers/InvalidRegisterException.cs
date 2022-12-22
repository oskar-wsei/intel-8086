namespace Emulator.Core.Components.Registers;

internal class InvalidRegisterException : EmulationException
{
    public InvalidRegisterException(byte registerId)
        : base($"Invalid register id: {registerId}") { }
}
