using System.Transactions;
using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Common.Endpoints.Transactions;

public class GetTransactionByPeriodEndpoint :IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Transactions : Get All")
            .WithSummary("Recupera todas as transações")
            .WithDescription("Recupera todas as transações")
            .WithOrder(5)
            .Produces<PagedResponse<List<Transaction>?>>();

    private static async Task<IResult> HandleAsync(ITransactionHandler handler, 
        [FromQuery]DateTime? startDate = null, 
        [FromQuery]DateTime? endDate = null,
        [FromQuery]int pageNumber = Configurations.DefaultPageNumber,
        [FromQuery]int pageSize = Configurations.DefaultPageSize
        )
    {
        var request = new GetTransactionsByPeriodRequest
        {
            UserId = "novo3",
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };
        var result = await handler.GetByPeriodAsync(request);

        return result.IsSucess
            ? TypedResults.Ok(result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}