namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    public Guid Id { get; private set; }
    public Guid SaleId { get; private set; }
    public Guid ProductId { get; private set; }

    public string ProductName { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount => (Quantity * UnitPrice) - Discount;
    public bool IsCancelled { get; private set; }

    protected SaleItem()
    {
    }

    internal SaleItem(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        IsCancelled = false;

        SetQuantity(quantity);
    }

    internal void SetQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity should be greater than zero.");

        if (quantity > 20)
            throw new InvalidOperationException($"It is not allowed to sell more than 20 items of the product {ProductName}.");

        Quantity = quantity;
        CalculateDiscount();
    }

    private void CalculateDiscount()
    {
        decimal totalBeforeDiscount = Quantity * UnitPrice;

        if (Quantity >= 10 && Quantity <= 20)
        {
            Discount = totalBeforeDiscount * 0.20m;
            return;
        }

        if (Quantity >= 4 && Quantity <= 9)
        {
            Discount = totalBeforeDiscount * 0.10m;
            return;
        }

        Discount = 0;
        return;
    }

    internal void Cancel()
    {
        IsCancelled = true;
    }


}
