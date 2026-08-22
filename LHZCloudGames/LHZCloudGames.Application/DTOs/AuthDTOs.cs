namespace LHZCloudGames.Application.DTOs;

public record RegisterUserRequest(string Name, string Email, string Password, bool IsAdmin = false);
public record LoginRequest(string Email, string Password);
public record AuthResponse(string Token, string Name, string Email, string Role);
