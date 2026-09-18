using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSaleHandler> _logger;

    public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<UpdateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null)
            throw new KeyNotFoundException($"Sale with id {request.Id} not found");

        if (sale.IsCancelled)
            throw new InvalidOperationException("Cannot update a cancelled sale.");

        sale.ClearItems();

        foreach (var item in request.Items)
        {
            sale.AddItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);
        }

        var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

        _logger.LogInformation("Integration Event Published: SaleModified - SaleId: {SaleId}", sale.Id);

        return _mapper.Map<UpdateSaleResult>(updatedSale);
    }
}
