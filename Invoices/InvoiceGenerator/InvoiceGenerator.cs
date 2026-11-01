// Source for the article "Generate invoices programmatically in .NET".
using System.Globalization;
using DOM;
using DOM.Abstracts;
using DOM.Elements.Graphics.Colors;
using DOM.Elements.TextElements;
using DOM.Enums;
using DOM.MeasurementUnits;
using DOM.Elements.Graphics.Effects;
using DOM.Styles;
using DOM.Elements.TableElements;
using ExampleSupport;

public static class InvoiceGenerator
{
    public static VDocs Generate(Invoice invoice)
    {
        // #region create-document
        var doc = new VDocs { LicensePath = ExampleLicense.Path };
        // #endregion

        // #region logo
        var header = doc.AddHeader(HeaderFooterType.Default);
        var logo = header.AddImage(ExampleAssets.PathFor("logo.png"));
        logo.SetDimensions(120, 40, Units.Pt);
        // #endregion

        // #region header
        var brand = new Color("#1E3A8A");
        var muted = new Color("#6B7280");

        TextStyle Style(double size, bool bold = false, Color? color = null) => new TextStyle
        {
            Font = new Font
            {
                Latin = new FontPart { FontName = "Calibri", Size = new FontSize(size), IsBold = bold },
                Color = color,
            },
        };

        Text Txt(string value, TextStyle style) => new Text(value) { Style = style };

        doc.AddParagraph().AddText(Txt(invoice.Seller.Name, Style(20, bold: true, color: brand)));
        doc.AddParagraph().AddText(Txt($"{invoice.Seller.Address} · {invoice.Seller.Email}", Style(9, color: muted)));
        doc.AddParagraph().AddText(Txt($"INVOICE {invoice.Number}", Style(16, bold: true)));
        doc.AddParagraph().AddText(Txt($"Issued {invoice.IssueDate:d MMM yyyy} · Due {invoice.DueDate:d MMM yyyy}", Style(10)));
        doc.AddParagraph().AddText(Txt($"Bill to: {invoice.Customer.Name}, {invoice.Customer.Address}", Style(10)));
        // #endregion

        // #region line-items
        var culture = CultureInfo.GetCultureInfo(invoice.Culture);
        string Money(decimal value) => value.ToString("C", culture);

        var subtotal = invoice.Lines.Sum(l => l.Amount);
        var taxable  = subtotal - invoice.Discount;
        var tax      = Math.Round(taxable * invoice.TaxRate, 2, MidpointRounding.AwayFromZero);
        var total    = taxable + tax;

        var summary = new (string Label, decimal Value, bool Bold)[]
        {
            ("Subtotal", subtotal, false),
            ("Discount", -invoice.Discount, false),
            ($"Tax ({invoice.TaxRate:P0})", tax, false),
            ("Total due", total, true),
        };

        int rows = 1 + invoice.Lines.Count + summary.Length;
        var table = new Table(rows, 4)
        {
            Style = new TableStyle
            {
                Layout = TableLayoutValues.Fixed,
                TableGridColsWidth = new List<DocumentUnit>
                {
                    new DocumentUnit(3.4, Units.Inch), new DocumentUnit(0.8, Units.Inch),
                    new DocumentUnit(1.1, Units.Inch), new DocumentUnit(1.2, Units.Inch),
                },
            },
        };

        var rightAligned = new ParagraphStyle { Justification = JustificationValues.Right };
        var headerShade  = new TableCellStyle
        {
            Shading = new Shading { ShadingPatternValue = ShadingPatternValues.Solid, BackgroundColor = brand },
        };

        void Cell(int row, int col, string value, TextStyle style, bool right = false)
        {
            var p = new Paragraph { Style = right ? rightAligned : null };
            p.AddText(Txt(value, style));
            table[row, col].SetContent(p);   // replaces the cell's default empty paragraph
        }

        // Header row
        string[] headers = { "Description", "Qty", "Unit price", "Amount" };
        for (int c = 0; c < headers.Length; c++)
        {
            table[0, c].Style = headerShade;
            Cell(0, c, headers[c], Style(10, bold: true, color: new Color("#FFFFFF")), right: c > 0);
        }

        // Line items
        for (int i = 0; i < invoice.Lines.Count; i++)
        {
            var line = invoice.Lines[i];
            Cell(i + 1, 0, line.Description, Style(10));
            Cell(i + 1, 1, line.Quantity.ToString(culture), Style(10), right: true);
            Cell(i + 1, 2, Money(line.UnitPrice), Style(10), right: true);
            Cell(i + 1, 3, Money(line.Amount), Style(10), right: true);
        }

        // Subtotal, discount, tax and total
        for (int s = 0; s < summary.Length; s++)
        {
            int r = 1 + invoice.Lines.Count + s;
            // The label goes in the "Unit price" column next to the amount.
            Cell(r, 2, summary[s].Label, Style(10, bold: summary[s].Bold), right: true);
            Cell(r, 3, Money(summary[s].Value), Style(summary[s].Bold ? 12 : 10, bold: summary[s].Bold), right: true);
        }

        doc.AddTable(table);
        // #endregion

        // #region seasonal
        if (invoice.IssueDate.Month == 12)
            doc.AddParagraph().AddText(Txt($"Thank you for a great year, {invoice.Customer.Name}!", Style(10, color: brand)));
        // #endregion

        // #region footer
        doc.AddParagraph();
        doc.AddParagraph().AddText(Txt("Payment terms", Style(11, bold: true)));
        doc.AddParagraph().AddText(Txt($"Please pay {Money(total)} by {invoice.DueDate:d MMMM yyyy}.", Style(10)));
        doc.AddParagraph().AddText(Txt("Bank transfer: Northwind Software Ltd. · IBAN GB00 0000 0000 0000 00 · Ref " + invoice.Number, Style(10)));
        doc.AddParagraph().AddText(Txt("Questions? Contact " + invoice.Seller.Email, Style(10, color: muted)));

        doc.AddFooter(HeaderFooterType.Default)
           .AddParagraph()
           .AddText(Txt($"{invoice.Seller.Name} · Invoice {invoice.Number}", Style(8, color: muted)));
        // #endregion

        return doc;
    }
}
