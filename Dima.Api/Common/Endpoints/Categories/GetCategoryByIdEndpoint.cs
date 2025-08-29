using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Common.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Categories : Get By Id")
            .WithSummary("Recupera uma categoria")
            .WithDescription("Recupera uma categoria")
            .WithOrder(4)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
    {
        var request = new GetCategoryByIdRequest
        {
            UserId = "novo3",
            Id = id
        };
        var result = await handler.GetByIdAsync(request);

        return result.IsSucess
            ? TypedResults.Ok(result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}