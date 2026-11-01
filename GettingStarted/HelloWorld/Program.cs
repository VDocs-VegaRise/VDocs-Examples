// Gallery: "Hello World" (site /examples and the landing page).
using DOM;
using DOM.Abstracts;
using DOM.Elements.Graphics.Colors;
using DOM.Elements.TextElements;
using DOM.Enums;
using DOM.MeasurementUnits;
using DOM.Styles;
using ExampleSupport;

// #region hello-world
using var doc = new VDocs();
doc.LicensePath = ExampleLicense.Path;

doc.AddParagraph().AddText("Hello, World!").Style = new TextStyle
{
    Font = new Font
    {
        Latin = new FontPart { FontName = "Arial", Size = new FontSize(24), IsBold = true },
        Color = new Color("#0000FF"),
    },
};

doc.AddParagraph("This is my first VDocs document.");

doc.SaveAsWord(ExampleOutput.PathFor("hello-world.docx"));
doc.SaveAsPdf(ExampleOutput.PathFor("hello-world.pdf"));
doc.SaveAsHtml(ExampleOutput.PathFor("hello-world.html"));
// #endregion
