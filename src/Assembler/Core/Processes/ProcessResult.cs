namespace Assembler.Core.Processes;

public struct ProcessResult
{
    public string Output;
    public string Errors;

    public bool HasErrors()
    {
        return Errors.Trim() != "";
    }
}
