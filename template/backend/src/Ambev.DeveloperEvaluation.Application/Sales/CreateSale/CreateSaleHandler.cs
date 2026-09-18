using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    private readonly ILogger _logger


    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = new Sale(
                request.SaleNumber,
                request.CustomerId,
                request.CustomerName,
                request.BranchId,
                request.BranchName
            );

        foreach (var item in request.Items)
        {
            sale.AddItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);
        }

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        _logger.LogInformation("Integration Event Published: SaleCreated - SaleId: {SaleId}", sale.Id);

        return _mapper.Map<CreateSaleResult>(createdSale);
    }


}
