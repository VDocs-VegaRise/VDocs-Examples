using ExampleSupport;

// #region sample-data
var invoice = new Invoice(
    Number: "INV-2026-1042",
    IssueDate: DateTime.Today,
    DueDate: DateTime.Today.AddDays(30),
    Seller: new Party("Northwind Software Ltd.", "1 Market Street, London", "billing@northwind.example"),
    Customer: new Party("Contoso Retail", "200 Main Avenue, Seattle", "ap@contoso.example"),
    Lines: new[]
    {
        new InvoiceLine("Platform subscription (October)", 1, 499m),
        new InvoiceLine("Additional seats", 12, 15m),
        new InvoiceLine("Onboarding workshop", 4, 120m),
    },
    Discount: 50m,
    TaxRate: 0.20m);
// #endregion

// #region save
using var doc = InvoiceGenerator.Generate(invoice);
doc.SaveAsPdf(ExampleOutput.PathFor($"{invoice.Number}.pdf"));
doc.SaveAsWord(ExampleOutput.PathFor($"{invoice.Number}.docx"));
// #endregion
