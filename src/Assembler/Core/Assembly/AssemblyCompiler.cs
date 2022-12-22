using System;
using System.Collections.Generic;
using System.IO;
using Assembler.Core.Processes;
using Assembler.Properties;

namespace Assembler.Core.Assembly;

public class AssemblyCompiler
{
    private readonly string _nasmPath = Path.Combine(Path.GetTempPath(), "__nasm.exe");
    private readonly string _sourcePath = Path.Combine(Path.GetTempPath(), "__asm_temp.asm");
    private readonly string _codePath = Path.Combine(Path.GetTempPath(), "__asm_temp");
    private readonly string _debugPath = Path.Combine(Path.GetTempPath(), "__asm_temp.dbg");

    public AssemblyCompiler()
    {
        File.WriteAllBytes(_nasmPath, Resources.Nasm);
    }

    public AssemblyBundle Compile(string source)
    {
        return new AssemblyBundle
        {
            Code = NasmMustCompile(source),
            SourceMap = NasmGenerateSourceMap(source),
        };
    }

    private byte[] NasmMustCompile(string source, int maxAttempts = 3)
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            try
            {
                return NasmCompile(source);
            }
            catch (AssemblyException e)
            {
                throw e;
            }
            catch (Exception)
            {
                continue;
            }
        }

        throw new AssemblyException("Could not compile");
    }

    private byte[] NasmCompile(string source)
    {
        File.WriteAllText(_sourcePath, "[bits 16]\n" + source);

        var result = ProcessUtils.Run(_nasmPath, _sourcePath);

        if (result.HasErrors())
        {
            throw new AssemblyException(CorrectLineNumberInError(result.Errors));
        }

        return File.ReadAllBytes(_codePath);
    }

    private Dictionary<int, int> NasmGenerateSourceMap(string source)
    {
        var debugInfoLines = NasmGenerateDebugInfo(source);
        var sourceMap = new Dictionary<int, int>();

        var byteOffset = 0;

        foreach (var line in debugInfoLines)
        {
            if (line.StartsWith("dbglinenum "))
            {
                var lineNumber = int.Parse(line.Replace(_sourcePath, "").Split(" ")[1][1..^1]) - 1;
                sourceMap[byteOffset] = lineNumber;
            }
            else if (line.StartsWith("out "))
            {
                var sizeAttribute = line[line.IndexOf("size ")..];
                var parts = sizeAttribute.Split(" ");

                byteOffset += int.Parse(parts[1]);
            }
        }

        return sourceMap;
    }

    private string[] NasmGenerateDebugInfo(string source)
    {
        File.WriteAllText(_sourcePath, "[bits 16]\n" + source);

        var result = ProcessUtils.Run(_nasmPath, _sourcePath + " -g -f dbg");

        if (result.HasErrors())
        {
            throw new AssemblyException(CorrectLineNumberInError(result.Errors));
        }

        return File.ReadAllLines(_debugPath);
    }

    private string CorrectLineNumberInError(string error)
    {
        var parts = error.Replace(_sourcePath + ":", "").Split(":", 2);
        var line = int.Parse(parts[0]) - 1;
        return line.ToString() + ":" + parts[1];
    }
}
