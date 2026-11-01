# VDocs Examples

Runnable C# examples for [VDocs](https://www.nuget.org/packages/VDocs), the .NET library for creating and converting Word, PDF and HTML documents.

Each folder is a small console project that builds one document and writes it to an `output` folder next to the project.

| Example | What it shows |
|---|---|
| [GettingStarted/HelloWorld](GettingStarted/HelloWorld) | A styled heading and a paragraph, saved as Word, PDF and HTML |
| [Formatting/StyledText](Formatting/StyledText) | Bold, italic and coloured text runs in one paragraph |
| [Reports/QuarterlyReport](Reports/QuarterlyReport) | Headings with outline levels |
| [Tables/EmployeeDirectory](Tables/EmployeeDirectory) | A table built cell by cell, and one built from a list of records |
| [Images/ProductCatalogue](Images/ProductCatalogue) | Adding and sizing an image |
| [Invoices/InvoiceGenerator](Invoices/InvoiceGenerator) | A complete invoice: logo header, styled table, totals, footer |
| [MailMerge/MergeLetter](MailMerge/MergeLetter) | Filling `{{Placeholders}}` in a Word template |
| [MailMerge/BulkMerge](MailMerge/BulkMerge) | One PDF per recipient from the same template |
| [Conversion/ConvertAndExport](Conversion/ConvertAndExport) | Opening a Word file and exporting it to Word, PDF, HTML and a stream |
| [Pdf/GeneratePdf](Pdf/GeneratePdf) | Paragraphs, a table, and saving as PDF, Word and HTML |
| [Word/WordWithoutOpenXml](Word/WordWithoutOpenXml) | Reusable text styles and a quick invoice table |

Input files (images, templates) live in [Assets](Assets).

To build and run every example and check it produced output: `scripts/run-all.sh`.

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
