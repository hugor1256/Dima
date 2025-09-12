using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Api.Models;

namespace Dima.Api.Common.Endpoints.Identity;

public class GetRolesEndPoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/roles", Handle).RequireAuthorization();
    
    private static Task<IResult> Handle(ClaimsPrincipal user)
    {
        if (user.Identity == null || !user.Identity.IsAuthenticated)
            return Task.FromResult(Results.Unauthorized());

        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType).Select(s => new
        {
            s.Issuer,
            s.OriginalIssuer,
            s.Type,
            s.Value,
            s.ValueType
        });

        return Task.FromResult<IResult>(TypedResults.Json(roles));
    }
}