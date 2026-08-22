using LHZCloudGames.Application.DTOs;
using LHZCloudGames.Domain.Entities;
using LHZCloudGames.Domain.Interfaces;

namespace LHZCloudGames.Application.Services;

public class GameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<GameResponse> CreateGameAsync(CreateGameRequest request)
    {
        var game = new Game(request.Title, request.Description, request.Price, request.ReleaseDate);
        await _gameRepository.AddAsync(game);
        await _gameRepository.SaveChangesAsync();

        return new GameResponse(game.Id, game.Title, game.Description, game.Price, game.ReleaseDate);
    }

    public async Task<IEnumerable<GameResponse>> GetAllGamesAsync()
    {
        var games = await _gameRepository.GetAllAsync();
        return games.Select(g => new GameResponse(g.Id, g.Title, g.Description, g.Price, g.ReleaseDate));
    }
}
