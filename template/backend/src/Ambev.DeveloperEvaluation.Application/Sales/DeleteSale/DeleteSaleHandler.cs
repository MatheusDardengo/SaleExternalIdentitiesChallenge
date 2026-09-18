using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ILogger<DeleteSaleHandler> _logger;

    public DeleteSaleHandler(ISaleRepository saleRepository, ILogger<DeleteSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _logger = logger;
    }

    public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null)
            throw new KeyNotFoundException($"Sale with id {request.Id} not found");

        sale.CancelSale();

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        _logger.LogInformation("Integration Event Published: SaleCancelled - SaleId: {SaleId}", request.Id);

        return new DeleteSaleResult { Success = true };
    }
}