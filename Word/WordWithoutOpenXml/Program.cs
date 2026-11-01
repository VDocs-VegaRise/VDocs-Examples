// Source for the article "Generate Word documents in .NET without Open XML".
using DOM;
using DOM.Abstracts;
using DOM.Elements.Graphics.Colors;
using DOM.Elements.TextElements;
using DOM.Enums;
using DOM.MeasurementUnits;
using DOM.Styles;
using DOM.Elements.TableElements;
using ExampleSupport;

using var doc = new VDocs();
doc.LicensePath = ExampleLicense.Path;

// #region hello-styled
doc.AddParagraph().AddText("Hello, World!").Style = new TextStyle
{
    Font = new Font
    {
        Latin = new FontPart { FontName = "Calibri", Size = new FontSize(14), IsBold = true },
        Color = new Color("#00008B"),
    },
};
// #endregion

// #region reusable-style
var heading = new TextStyle
{
    Font = new Font
    {
        Latin = new FontPart { FontName = "Calibri Light", Size = new FontSize(20), IsBold = true },
        Color = new Color("#00008B"),
    },
};

doc.AddParagraph().AddText(new Text("Chapter 1") { Style = heading });
doc.AddParagraph().AddText(new Text("Chapter 2") { Style = heading });
// #endregion

// #region invoice
var items = new[]
{
    new LineItem("Consulting services", 10, 120m),
    new LineItem("Support package", 1, 300m),
};

var title = new TextStyle
{
    Font = new Font
    {
        Latin = new FontPart { FontName = "Calibri", Size = new FontSize(22), IsBold = true },
        Color = new Color("#00008B"),
    },
};

doc.AddParagraph().AddText(new Text("Invoice #1042") { Style = title });
doc.AddParagraph().AddText("Bill to: Contoso Ltd.");
doc.AddParagraph().AddText($"Date: {DateTime.Today:MMMM d, yyyy}");

// One row per item, with a header row generated from the property names
var table = Table.FromEnumerable(items, includeHeaders: true);
doc.AddTable(table);

var total = items.Sum(i => i.Quantity * i.UnitPrice);
doc.AddParagraph().AddText(new Text($"Total: {total:C}")
{
    Style = new TextStyle
    {
        Font = new Font { Latin = new FontPart { FontName = "Calibri", Size = new FontSize(12), IsBold = true } },
    },
});

doc.SaveAsWord(ExampleOutput.PathFor("invoice.docx"));
doc.SaveAsPdf(ExampleOutput.PathFor("invoice.pdf"));   // same document, as a PDF
// #endregion

// #region line-item
public record LineItem(string Description, int Quantity, decimal UnitPrice);
// #endregion
