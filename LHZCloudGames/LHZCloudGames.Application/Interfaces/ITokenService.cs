using LHZCloudGames.Domain.Entities;

namespace LHZCloudGames.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
