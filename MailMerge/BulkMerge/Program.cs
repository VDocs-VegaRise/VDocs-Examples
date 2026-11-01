// Gallery: "Bulk Mail Merge". One document per recipient from the same template.
using System.Globalization;
using DOM;
using ExampleSupport;

var orders = new[]
{
    new Order("Jane Smith", "June 1, 2024", 549.00m, "ORD-8821"),
    new Order("Omar Haddad", "June 2, 2024", 120.50m, "ORD-8822"),
    new Order("Li Wei", "June 3, 2024", 1999.99m, "ORD-8823"),
};

// #region bulk-merge
var templatePath = ExampleAssets.PathFor("letter-template.docx");
var culture = CultureInfo.GetCultureInfo("en-US");

foreach (var order in orders)
{
    using var doc = VDocs.OpenFromWord(templatePath);
    doc.LicensePath = ExampleLicense.Path;

    foreach (var run in doc.Paragraphs.SelectMany(p => p.Texts))
    {
        run.Content = run.Content
            .Replace("{{RecipientName}}", order.RecipientName)
            .Replace("{{Date}}", order.Date)
            .Replace("{{Amount}}", order.Amount.ToString("C", culture))
            .Replace("{{OrderRef}}", order.OrderRef);
    }

    doc.SaveAsPdf(ExampleOutput.PathFor($"confirmation-{order.OrderRef}.pdf"));
}
// #endregion

public record Order(string RecipientName, string Date, decimal Amount, string OrderRef);
