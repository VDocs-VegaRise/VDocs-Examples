// Gallery: "Report". Also the VDocs side of the article "Stop using Word Interop".
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

// #region report
TextStyle Font(double size, bool bold = false) => new TextStyle
{
    Font = new Font { Latin = new FontPart { FontName = "Calibri", Size = new FontSize(size), IsBold = bold } },
};

// Outline levels make the paragraphs real headings (navigation pane, table of contents).
var title = doc.AddParagraph();
title.Style = new ParagraphStyle { OutlineLevel = OutlineLevelValues.Level1 };
title.AddText("Quarterly Report").Style = Font(20, bold: true);

var section = doc.AddParagraph();
section.Style = new ParagraphStyle { OutlineLevel = OutlineLevelValues.Level2 };
section.AddText("Executive Summary").Style = Font(14, bold: true);

doc.AddParagraph().AddText("Revenue grew 24% year-over-year.").Style = Font(11);

doc.SaveAsWord(ExampleOutput.PathFor("report.docx"));
doc.SaveAsPdf(ExampleOutput.PathFor("report.pdf"));
// #endregion
