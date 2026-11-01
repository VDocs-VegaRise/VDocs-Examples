# VDocs Examples

Runnable C# examples for [VDocs](https://www.nuget.org/packages/VDocs), the .NET library for creating and converting Word, PDF and HTML documents.

Each folder is a small console project that builds one document and writes it to an `output` folder next to the project.

| Example | What it shows |
|---|---|
| [GettingStarted/HelloWorld](GettingStarted/HelloWorld) | The smallest possible document |
| [Pdf/GeneratePdf](Pdf/GeneratePdf) | Paragraphs, a table, and saving as PDF, Word and HTML |

## Running an example

1. **Get a license.** VDocs needs a license file. You can create a free 1-month license.
   Use **`VDocs.Examples`** as the project name: every example runs as an application with that name, so one license covers all of them.
2. Save the license as `vdocs.lic` in the root of this repository (it is git-ignored),
   or set the `VDOCS_LICENSE_PATH` environment variable to its location.
3. Run an example:

   ```shell
   cd Pdf/GeneratePdf
   dotnet run
   ```

## Repository layout

- `Directory.Build.props`: settings shared by all examples, including the VDocs version.
- `Shared/`: small helpers for finding the license and the output folder. They are not part of VDocs.
- `// #region name` … `// #endregion` markers: they mark the snippets shown on the VDocs website. Keep them when editing.

## For VDocs maintainers

If the VDocs source repository is checked out next to this one (`../VDocs`), the examples build against the source instead of the NuGet package. That catches API changes before a release. Force a mode with:

```shell
dotnet build -p:UseLocalVDocs=true    # build against ../VDocs/lib/VDocs
dotnet build -p:UseLocalVDocs=false   # build against the NuGet package
```

Debug builds of the VDocs source skip license validation, so no license is needed in that mode.
