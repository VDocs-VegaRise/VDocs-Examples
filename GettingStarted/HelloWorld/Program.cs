using DOM;
using ExampleSupport;

// #region hello-world
using var doc = new VDocs();
doc.LicensePath = ExampleLicense.Path;

doc.AddParagraph("Hello, World!");
doc.AddParagraph("This is my first VDocs document.");

doc.SaveAsWord(ExampleOutput.PathFor("hello-world.docx"));
// #endregion
