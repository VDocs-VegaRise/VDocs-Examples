namespace ExampleSupport;

/// <summary>
/// Locates the VDocs license file used by the examples.
/// </summary>
/// <remarks>
/// Lookup order:
/// 1. The VDOCS_LICENSE_PATH environment variable.
/// 2. A file named "vdocs.lic" in the current directory or any parent directory
///    (put it in the root of this repository; it is git-ignored).
/// </remarks>
internal static class ExampleLicense
{
    private const string FileName = "vdocs.lic";
    private const string EnvironmentVariable = "VDOCS_LICENSE_PATH";

    public static string Path => _path ??= Find();

    private static string? _path;

    private static string Find()
    {
        var fromEnvironment = Environment.GetEnvironmentVariable(EnvironmentVariable);
        if (!string.IsNullOrWhiteSpace(fromEnvironment))
            return fromEnvironment;

        for (var dir = new DirectoryInfo(Directory.GetCurrentDirectory()); dir != null; dir = dir.Parent)
        {
            var candidate = System.IO.Path.Combine(dir.FullName, FileName);
            if (File.Exists(candidate))
                return candidate;
        }

        // Not found: return the expected location so VDocs reports a clear
        // "license file not found" error, and print how to get one.
        Console.Error.WriteLine(
            $"No VDocs license found. Create a free 1-month license for the project name " +
            $"\"VDocs.Examples\", save it as '{FileName}' in the root of this repository, " +
            $"or set {EnvironmentVariable} to its path.");
        return System.IO.Path.Combine(Directory.GetCurrentDirectory(), FileName);
    }
}
