using LHZCloudGames.Domain.Entities;

namespace LHZCloudGames.Domain.Interfaces;

public interface IGameRepository
{
    Task<Game?> GetByIdAsync(Guid id);
    Task<IEnumerable<Game>> GetAllAsync();
    Task AddAsync(Game game);
    Task SaveChangesAsync();
}
