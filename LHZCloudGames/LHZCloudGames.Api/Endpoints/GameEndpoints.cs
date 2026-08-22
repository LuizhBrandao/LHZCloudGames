using LHZCloudGames.Application.DTOs;
using LHZCloudGames.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace LHZCloudGames.Api.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/games").WithTags("Games").RequireAuthorization();

        group.MapGet("/", async (GameService gameService) => 
        {
            var games = await gameService.GetAllGamesAsync();
            return Results.Ok(games);
        });

        group.MapPost("/", [Authorize(Roles = "Administrator")] async (CreateGameRequest request, GameService gameService) => 
        {
            var game = await gameService.CreateGameAsync(request);
            return Results.Created($"/api/games/{game.Id}", game);
        });
    }
}
