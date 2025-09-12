using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Common.Endpoints.Transactions;

public class GetTransactionByIdEndpoint :IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Transactions : Get By Id")
            .WithSummary("Recupera uma Transação")
            .WithDescription("Recupera uma Transação")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITransactionHandler handler, 
        long id)
    {
        var request = new GetTransactionByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };
        var result = await handler.GetByIdAsync(request);

        return result.IsSucess
            ? TypedResults.Ok(result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}