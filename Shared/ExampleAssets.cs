namespace ExampleSupport;

/// <summary>
/// Finds input files (images, templates) in the repository's Assets folder.
/// </summary>
internal static class ExampleAssets
{
    public static string PathFor(string fileName)
    {
        for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir != null; dir = dir.Parent)
        {
            var candidate = Path.Combine(dir.FullName, "Assets", fileName);
            if (File.Exists(candidate))
                return candidate;
        }

        throw new FileNotFoundException($"Asset '{fileName}' not found in the repository's Assets folder.");
    }
}
