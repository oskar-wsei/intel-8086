using System.Collections.Generic;

namespace Assembler.Core.Assembly;

public struct AssemblyBundle
{
    public byte[] Code;
    public Dictionary<int, int> SourceMap;
}
