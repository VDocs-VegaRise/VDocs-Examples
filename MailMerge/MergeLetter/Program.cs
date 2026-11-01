// Gallery: "Mail Merge". Fills {{Placeholders}} in a Word template.
using DOM;
using ExampleSupport;

// #region merge
var values = new Dictionary<string, string>
{
    ["RecipientName"] = "Jane Smith",
    ["Date"]          = "June 1, 2024",
    ["Amount"]        = "$549.00",
    ["OrderRef"]      = "ORD-8821",
};

using var doc = VDocs.OpenFromWord(ExampleAssets.PathFor("letter-template.docx"));
doc.LicensePath = ExampleLicense.Path;

// Placeholders are replaced per text run. Keep each {{Placeholder}} in a single
// run in your template (type it in one go, without changing formatting halfway).
foreach (var paragraph in doc.Paragraphs)
{
    foreach (var run in paragraph.Texts)
    {
        foreach (var (key, value) in values)
            run.Content = run.Content.Replace("{{" + key + "}}", value);
    }
}

doc.SaveAsWord(ExampleOutput.PathFor("confirmation.docx"));
// #endregion
