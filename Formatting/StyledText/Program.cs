// Gallery: "Styled Text". Mixes bold, italic and coloured runs in one paragraph.
using DOM;
using DOM.Abstracts;
using DOM.Elements.Graphics.Colors;
using DOM.Elements.TextElements;
using DOM.Enums;
using DOM.MeasurementUnits;
using DOM.Styles;
using ExampleSupport;

using var doc = new VDocs();
doc.LicensePath = ExampleLicense.Path;

// #region styled-runs
TextStyle Style(bool bold = false, bool italic = false, Color? color = null) => new TextStyle
{
    Font = new Font
    {
        Latin = new FontPart { FontName = "Calibri", Size = new FontSize(11), IsBold = bold, IsItalic = italic },
        Color = color,
    },
};

doc.AddParagraph(
    new Text("Important: ") { Style = Style(bold: true) },
    new Text("Please review the terms "),
    new Text("carefully") { Style = Style(italic: true) },
    new Text(" before proceeding."));

doc.AddParagraph(
    new Text("Status: "),
    new Text("Approved") { Style = Style(bold: true, color: new Color("#16A34A")) });
// #endregion

doc.SaveAsWord(ExampleOutput.PathFor("styled.docx"));
doc.SaveAsPdf(ExampleOutput.PathFor("styled.pdf"));
