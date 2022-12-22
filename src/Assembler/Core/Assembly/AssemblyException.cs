using System;

namespace Assembler.Core.Assembly;

public class AssemblyException : Exception
{
    public AssemblyException()
        : base() { }

    public AssemblyException(string? message)
        : base(message) { }

    public AssemblyException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
