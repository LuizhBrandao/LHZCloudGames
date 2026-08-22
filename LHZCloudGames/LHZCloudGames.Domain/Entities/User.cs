using LHZCloudGames.Domain.Enums;
using System.Text.RegularExpressions;

namespace LHZCloudGames.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }
    
    public ICollection<UserGame> AcquiredGames { get; private set; } = new List<UserGame>();

    protected User() { } // For EF Core

    public User(string name, string email, string passwordHash, UserRole role = UserRole.User)
    {
        Id = Guid.NewGuid();
        Name = name;
        SetEmail(email);
        PasswordHash = passwordHash;
        Role = role;
    }

    public void SetEmail(string email)
    {
        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email format.");
        Email = email;
    }

    public static bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8) return false;
        
        bool hasLetter = Regex.IsMatch(password, @"[a-zA-Z]");
        bool hasNumber = Regex.IsMatch(password, @"[0-9]");
        bool hasSpecial = Regex.IsMatch(password, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        return hasLetter && hasNumber && hasSpecial;
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
    }
}
