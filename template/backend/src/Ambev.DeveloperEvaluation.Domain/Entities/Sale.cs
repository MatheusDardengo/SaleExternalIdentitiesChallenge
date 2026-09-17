namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    public Guid Id { get; private set; }
    public string SaleNumber { get; private set; }
    public DateTime SaleDate { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid BranchId { get; private set; }
    public string BranchName { get; private set; }

    public bool IsCancelled { get; private set; }


    private readonly List<SaleItem> _items = new();
    public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();



    public decimal TotalSaleAmount => _items.Where(i => !i.IsCancelled).Sum(i => i.TotalAmount);

    protected Sale() { }

    public Sale(string saleNumber, Guid customerId, string customerName, Guid branchId, string branchName)
    {
        Id = Guid.NewGuid();
        SaleNumber = saleNumber;
        SaleDate = DateTime.UtcNow;
        CustomerId = customerId;
        BranchId = branchId;
        BranchName = branchName;
        IsCancelled = false;
    }

    public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Não é possível adicionar itens a uma venda cancelada.");

        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId && !i.IsCancelled);

        if (existingItem != null)
        {
            existingItem.SetQuantity(existingItem.Quantity + quantity);
        }
        else
        {
            _items.Add(new SaleItem(productId, productName, quantity, unitPrice));
        }
    }

    public void CancelSale()
    {
        if (IsCancelled) return;

        IsCancelled = true;

        foreach (var item in _items)
        {
            item.Cancel();
        }
    }

    public void CancelItem(Guid productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId && !i.IsCancelled);
        if (item == null)
            throw new InvalidOperationException("Item não encontrado na venda.");

        item.Cancel();
    }
}
