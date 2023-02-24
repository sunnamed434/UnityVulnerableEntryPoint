#nullable enable
namespace UnityVulnerableEntryPoint.CLI;

public class Options
{
    [Option('f', "file", Required = true, HelpText = "Set target file path.")]
    public string FilePath { get; set; }

    [Option('r', "references", Required = false,
        HelpText = "Set references of file, stay empty to specify the path from file path.")]
    public string? ReferencesPath { get; set; }
}