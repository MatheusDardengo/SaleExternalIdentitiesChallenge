using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using AutoMapper;
using static Ambev.DeveloperEvaluation.Application.Sales.CreateSale.CreateSaleCommand;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Initializes the mappings for CreateSale feature
/// </summary>
public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleRequestItem, CreateSaleCommandItem>();
    }
}
