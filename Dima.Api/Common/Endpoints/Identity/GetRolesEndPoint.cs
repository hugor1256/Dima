using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Api.Models;
using Dima.Core.Models.Account;

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
        var roles = identity.FindAll(identity.RoleClaimType).Select(s => new RoleClaim
        {
            Issuer = s.Issuer,
            OriginalIssuer = s.OriginalIssuer,
            Type = s.Type,
            Value = s.Value,
            ValueType = s.ValueType
        });

        return Task.FromResult<IResult>(TypedResults.Json(roles));
    }
}