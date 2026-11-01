// Source for the article "How to Generate a PDF in C# and .NET (Without Office)".
using DOM;
using DOM.Elements.TableElements;
using ExampleSupport;

// #region create-document
using var doc = new VDocs();
doc.LicensePath = ExampleLicense.Path;

doc.AddParagraph().AddText("Quarterly Report");
doc.AddParagraph().AddText("Revenue grew 24% year-over-year, driven by enterprise adoption.");
// #endregion

// #region add-table
var table = new Table(3, 2);
table[0, 0].AddText("Region");
table[0, 1].AddText("Revenue");
table[1, 0].AddText("EMEA");
table[1, 1].AddText("$1.2M");
table[2, 0].AddText("Americas");
table[2, 1].AddText("$2.4M");
doc.AddTable(table);
// #endregion

// #region save
doc.SaveAsPdf(ExampleOutput.PathFor("report.pdf"));

// Optional: the same document in other formats
doc.SaveAsWord(ExampleOutput.PathFor("report.docx"));
doc.SaveAsHtml(ExampleOutput.PathFor("report.html"));
// #endregion
