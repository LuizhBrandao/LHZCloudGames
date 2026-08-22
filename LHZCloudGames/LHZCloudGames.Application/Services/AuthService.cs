using BCrypt.Net;
using LHZCloudGames.Application.DTOs;
using LHZCloudGames.Application.Interfaces;
using LHZCloudGames.Domain.Entities;
using LHZCloudGames.Domain.Enums;
using LHZCloudGames.Domain.Interfaces;

namespace LHZCloudGames.Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterUserRequest request)
    {
        if (!User.IsValidPassword(request.Password))
            throw new ArgumentException("Password does not meet complexity requirements.");

        if (await _userRepository.GetByEmailAsync(request.Email) != null)
            throw new ArgumentException("Email is already in use.");

        string hash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var role = request.IsAdmin ? UserRole.Administrator : UserRole.User;
        
        var user = new User(request.Name, request.Email, hash, role);
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        var token = _tokenService.GenerateToken(user);
        return new AuthResponse(token, user.Name, user.Email, user.Role.ToString());
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials.");

        var token = _tokenService.GenerateToken(user);
        return new AuthResponse(token, user.Name, user.Email, user.Role.ToString());
    }
}
