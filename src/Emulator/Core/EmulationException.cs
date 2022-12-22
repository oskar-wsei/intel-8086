using System;
using System.Runtime.Serialization;

namespace Emulator.Core;

public class EmulationException : Exception
{
    public EmulationException()
        : base() { }

    public EmulationException(string? message)
        : base(message) { }

    public EmulationException(string? message, Exception? innerException)
        : base(message, innerException) { }

    protected EmulationException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
