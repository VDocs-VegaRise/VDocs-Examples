// Landing page: "Convert & Export". Opens an existing Word file and exports it.
using DOM;
using DOM.Abstracts;
using DOM.Elements.Graphics.Colors;
using DOM.Elements.TextElements;
using DOM.Enums;
using DOM.MeasurementUnits;
using DOM.Styles;
using ExampleSupport;

// #region convert
// Load an existing Word document
using var doc = VDocs.OpenFromWord(ExampleAssets.PathFor("report.docx"));
doc.LicensePath = ExampleLicense.Path;

// Modify content if needed
doc.AddParagraph().AddText("Appended content").Style = new TextStyle
{
    Font = new Font { Latin = new FontPart { FontName = "Arial", Size = new FontSize(12) } },
};

// Export to all three formats from the same DOM
doc.SaveAsWord(ExampleOutput.PathFor("report_updated.docx"));
doc.SaveAsPdf(ExampleOutput.PathFor("report.pdf"));
doc.SaveAsHtml(ExampleOutput.PathFor("report.html"));

// Or get HTML as a string for a web preview
string html = doc.GetHtml();

// Or write the PDF to a stream, for example an HTTP response
using var output = new MemoryStream();
doc.SaveAsPdf(output);
// #endregion

Console.WriteLine($"HTML: {html.Length} characters, PDF stream: {output.Length} bytes");
