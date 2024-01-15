using tic_tac_toe.Data;
using tic_tac_toe.Data.Repository;
using tic_tac_toe.Data.Service;
using tic_tac_toe.UI;
using tic_tac_toe.Utils;

namespace tic_tac_toe
{
  class Program
  {
    private static void Main(string[] args)
    {
      var dbContext = new DbContext();
      var usersRepository = new UsersRepository(dbContext);
      var usersService = new UsersService(usersRepository);

      var authUi = new AuthUI(usersService);

      RunGame(authUi);
    }

    private static void RunGame(AuthUI authUi)
    {
      var authUINext = (User.User activeUser) => new UserUI(activeUser).ShowUI();

      Dictionary<int, (string commandInfo, Action command)> uiCommands = new Dictionary<int, (string, Action)>
      {
        { 1, ("Реєстрація", () => authUi.RegisterUser(authUINext)) },
        { 2, ("Вхід", () => authUi.Login(authUINext)) },
        { 3, ("Вихід", () => Environment.Exit(0)) }
      };

      Console.WriteLine("Ласкаво просимо до гри у хрестики-ноліки!");
      ConsoleHelper.ShowMenu(() => true, uiCommands);
    }
  }
}