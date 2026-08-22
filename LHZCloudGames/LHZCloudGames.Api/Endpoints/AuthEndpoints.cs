using LHZCloudGames.Application.DTOs;
using LHZCloudGames.Application.Services;

namespace LHZCloudGames.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Authentication");

        group.MapPost("/register", async (RegisterUserRequest request, AuthService authService) => 
        {
            var response = await authService.RegisterAsync(request);
            return Results.Ok(response);
        });

        group.MapPost("/login", async (LoginRequest request, AuthService authService) => 
        {
            var response = await authService.LoginAsync(request);
            return Results.Ok(response);
        });
    }
}
