using System.Text;

namespace tic_tac_toe.User
{

  public class User
  {
    private readonly List<Game.Game> _games = new List<Game.Game>();
    private readonly string _password;
    public readonly int Id;
    public readonly string Name;
    private decimal _rating;

    public User(int id, string name, decimal initialRating, string password)
    {
      Id = id;
      Name = name;
      _rating = initialRating;
      _password = password;
    }


    public bool CheckPassword(string password)
    {
      return password == _password;
    }

    public void WinGame(Game.Game game)
    {
      _games.Add(game);
      _rating += game.Rating;
    }

    public void LoseGame(Game.Game game)
    {
      _games.Add(game);
      _rating = Math.Max(_rating - game.Rating, 0);
    }

    public void DrawGame(Game.Game game)
    {
      _games.Add(game);
    }

    public void GetStats()
    {
      Console.WriteLine($"Ваш рейтинг: {_rating}");

      var gamesCount = _games.Count;

      if (gamesCount <= 0)
      {
        Console.WriteLine("На цьому акануті ще нема історії ігор\n");
      }
      else
      {
        var history = new StringBuilder();

        history.AppendLine("Результат\tРейтинг\tІндекс");

        for (var i = 0; i < gamesCount; i++)
          history.AppendLine($"{i}\t{_games[i].Result}\t{_games[i].Rating}");

        Console.WriteLine(history.ToString());
      }
    }
  }
}