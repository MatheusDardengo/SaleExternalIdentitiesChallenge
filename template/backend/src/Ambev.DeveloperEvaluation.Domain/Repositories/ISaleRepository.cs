using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    /// <summary>
    /// Inser new sale at the database, including its items (SaleItems).
    /// </summary>
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Search sale by id.
    /// </summary>
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update existing sale (and its items).
    /// </summary>
    Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
}
