using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Common.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Categories : Get All")
            .WithSummary("Recupera todas as categoria")
            .WithDescription("Recupera todas as categoria")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, 
        [FromQuery]int pageNumber = Configurations.DefaultPageNumber, 
        [FromQuery]int pageSize = Configurations.DefaultPageSize)
    {
        var request = new GetAllCaregoriesRequest()
        {
            UserId = "novo3",
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await handler.GetAllAsync(request);

        return result.IsSucess
            ? TypedResults.Ok(result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}