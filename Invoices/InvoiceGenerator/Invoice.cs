// #region model
public record Party(string Name, string Address, string Email);

public record InvoiceLine(string Description, int Quantity, decimal UnitPrice)
{
    public decimal Amount => Quantity * UnitPrice;
}

public record Invoice(
    string Number, DateTime IssueDate, DateTime DueDate,
    Party Seller, Party Customer, IReadOnlyList<InvoiceLine> Lines,
    decimal Discount, decimal TaxRate, string Culture = "en-US");
// #endregion
