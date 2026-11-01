// Gallery: "Image". Adds an image and sets its size.
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

// #region add-image
doc.AddParagraph().AddText("Product catalogue");

var image = doc.AddImage(ExampleAssets.PathFor("product.png"));
image.SetDimensions(2, 2, Units.Inch);

doc.SaveAsWord(ExampleOutput.PathFor("catalogue.docx"));
doc.SaveAsPdf(ExampleOutput.PathFor("catalogue.pdf"));
// #endregion
