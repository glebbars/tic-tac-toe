using tic_tac_toe.Data.Service;

namespace tic_tac_toe.UI
{

  public class AuthUI
  {
    private readonly UsersService _usersService;

    public AuthUI(UsersService usersService)
    {
      _usersService = usersService;
    }

    public void RegisterUser(Action<User.User> next)
    {
      Console.Write("Введіть ім'я користувача: ");
      var username = Console.ReadLine();

      if (username == null)
      {
        Console.WriteLine("Будь ласка введіть коректне ім'я користувача");

        return;
      }

      Console.Write("Введіть пароль: ");
      var password = Console.ReadLine();

      if (password == null)
      {
        Console.WriteLine("Ви успішно зареєстровані!");

        return;
      }

      var createdUser = _usersService.CreateUser(username, 1000, password);
      Console.WriteLine("Ви успішно зареєстровані!");

      next(createdUser);
    }

    public void Login(Action<User.User> next)
    {
      Console.Write("Введіть ім'я користувача: ");
      var username = Console.ReadLine();

      if (username == null)
      {
        Console.WriteLine("Користувача з таким ім'ям не існує. Будь ласка, зареєструйтесь.");

        return;
      }

      var foundUser = _usersService.GetUserByName(username);

      if (foundUser == null)
      {
        Console.WriteLine("Користувача з заданим ім'ям не було знайдено");

        return;
      }

      Console.Write("Введіть пароль: ");
      var password = Console.ReadLine();

      if (!foundUser.CheckPassword(password))
      {
        Console.WriteLine("Неправильний пароль");

        return;
      }

      Console.WriteLine($"Ви увійшли як {foundUser.Name}.");

      next(foundUser);
    }
  }
}