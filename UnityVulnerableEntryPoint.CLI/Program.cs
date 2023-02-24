// ReSharper disable ForCanBeConvertedToForeach
// ReSharper disable InvertIf
#nullable enable

Console.WriteLine(
@$"
  __  __     _ __      _   __     __                 __   __    ____    __           ___       _     __
 / / / ___  (_/ /___ _| | / __ __/ ___ ___ _______ _/ /  / ___ / _____ / /_______ __/ _ \___  (____ / /_
/ /_/ / _ \/ / __/ // | |/ / // / / _ / -_/ __/ _ `/ _ \/ / -_/ _// _ / __/ __/ // / ___/ _ \/ / _ / __/
\____/_//_/_/\__/\_, /|___/\_,_/_/_//_\__/_/  \_,_/_.__/_/\__/___/_//_\__/_/  \_, /_/   \___/_/_//_\__/
                /___/                                                        /___/
https://github.com/sunnamed434/UnityVulnerableEntryPoint
UnityVulnerableEntryPoint v{FileVersionInfo.GetVersionInfo(typeof(Program).Assembly.Location).FileVersion}
");

try
{
    string? targetFileName = null;
    string? referencesDirectoryName = null;
    if (args.Any())
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(Start);
    }
    else
    {
        Console.Write("Enter path to the target file (for example Assembly-CSharp.dll): ");
        targetFileName = Console.ReadLine();

        Console.Write(@"Enter path to the references (for example of the ..\GameName\GameName_Data\Managed\..) or enter nothing to select directory of the specified file: ");
        referencesDirectoryName = Console.ReadLine();

        Start(new Options
        {
            FilePath = targetFileName,
            ReferencesPath = referencesDirectoryName
        });
    }
}
catch (Exception ex)
{
    Console.WriteLine("Something went wrong! " + ex);
}

Console.WriteLine("Enter something to exit...");
Console.ReadLine();

void Start(Options options)
{
    if (string.IsNullOrWhiteSpace(options.ReferencesPath))
    {
        options.ReferencesPath = Path.GetDirectoryName(options.FilePath);
    }

    var dependenciesData = Directory.GetFiles(options.ReferencesPath).Select(File.ReadAllBytes);
    var targetFileBytes = File.ReadAllBytes(options.FilePath);

    var moduleReaderParameters = new ModuleReaderParameters(EmptyErrorListener.Instance);
    var module = SerializedModuleDefinition.FromBytes(targetFileBytes, moduleReaderParameters);
    var assemblyResolver = module.MetadataResolver.AssemblyResolver;

    var signatureComparer = new SignatureComparer(SignatureComparisonFlags.AcceptNewerVersions);

    foreach (var originalReference in module.AssemblyReferences)
    {
        foreach (var data in dependenciesData)
        {
            try
            {
                var definition = AssemblyDefinition.FromBytes(data);
                if (assemblyResolver.HasCached(originalReference) == false && signatureComparer.Equals(originalReference, definition))
                {
                    assemblyResolver.AddToCache(originalReference, definition);
                    Console.WriteLine("[+] Resolved: " + originalReference.FullName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[-] Failed to resolve " + originalReference.FullName + ", perhaps this is not a .NET file! " + ex);
            }
        }
    }

    foreach (var type in module.GetAllTypes())
    {
        foreach (var method in type.Methods)
        {
            if (method.CilMethodBody is { } body)
            {
                var instructions = body.Instructions;
                for (var i = 0; i < instructions.Count; i++)
                {
                    var instruction = instructions[i];
                    if ((instruction.OpCode == CilOpCodes.Call || instruction.OpCode == CilOpCodes.Callvirt) && instruction.Operand is IMethodDescriptor descriptor)
                    {
                        var callingMethod = descriptor.Resolve();
                        if (callingMethod != null)
                        {
                            const string MonoBehaviourClassName = "MonoBehaviour";
                            if (callingMethod.DeclaringType?.BaseType?.Name == MonoBehaviourClassName && callingMethod.Module?.Name != module.Name)
                            {
                                Console.WriteLine($"[?] Potentially vulnerable entry point in method {callingMethod.Name} in {callingMethod.Module.Assembly.Name} assembly, called by {method.Name}");
                            }
                        }
                    }
                }
            }
        }
    }
}