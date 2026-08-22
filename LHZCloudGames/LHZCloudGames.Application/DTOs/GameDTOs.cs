namespace LHZCloudGames.Application.DTOs;

public record CreateGameRequest(string Title, string Description, decimal Price, DateTime ReleaseDate);
public record GameResponse(Guid Id, string Title, string Description, decimal Price, DateTime ReleaseDate);
