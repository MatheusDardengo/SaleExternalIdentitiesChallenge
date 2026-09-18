using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[Route("api/[controller]")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale.
    /// </summary>
    /// <param name="request">The sale creation request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateSaleCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new { Success = true, Message = "Sale created successfully.", Data = result });
    }

    /// <summary>
    /// Get sale by id.
    /// </summary>
    /// <param name="id">Unique identifier from sale.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleQuery(id);

        var result = await _mediator.Send(request, cancellationToken);

        return Ok(new { Success = true, Message = "Sales retrieved successfully.", Data = result });
    }

    /// <summary>
    /// Soft delete sale by id.
    /// </summary>
    /// <param name="id">Sale to be deleted id.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleCommand(id);

        var result = await _mediator.Send(request, cancellationToken);

        return Ok(new { Success = true, Message = "Sale deleted successfully.", Data = result });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateSaleCommand>(request);

        command.Id = id;

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(new { Success = true, Message = "Sale updated successfully", Data = result });
    }

}
