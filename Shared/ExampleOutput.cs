namespace ExampleSupport;

/// <summary>
/// Gives each example a predictable place to write its output files.
/// </summary>
internal static class ExampleOutput
{
    /// <summary>
    /// Returns a path inside the "output" folder of the current directory,
    /// creating the folder if needed.
    /// </summary>
    public static string PathFor(string fileName)
    {
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, fileName);
        Console.WriteLine($"Writing {path}");
        return path;
    }
}
