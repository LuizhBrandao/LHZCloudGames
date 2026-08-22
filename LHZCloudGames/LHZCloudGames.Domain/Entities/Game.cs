namespace LHZCloudGames.Domain.Entities;

public class Game
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public DateTime ReleaseDate { get; private set; }

    public ICollection<UserGame> UserGames { get; private set; } = new List<UserGame>();

    protected Game() { }

    public Game(string title, string description, decimal price, DateTime releaseDate)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Price = price;
        ReleaseDate = releaseDate;
    }
}
