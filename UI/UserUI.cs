using tic_tac_toe.Utils;

namespace tic_tac_toe.UI
{
  public class UserUI
  {
    private readonly User.User _user;
    private bool _keepRunning = true;

    public UserUI(User.User currentUser)
    {
      _user = currentUser;
    }

    public void ShowUI()
    {
      Dictionary<int, (string commandInfo, Action command)> uiCommands = new Dictionary<int, (string, Action)>
      {
        { 1, ("Play a game", PlayGame) },
        { 2, ("Show stats", GetStats) },
        { 3, ("Logout", Logout) }
      };

      ConsoleHelper.ShowMenu(() => _keepRunning, uiCommands);
    }

    private void PlayGame()
    {
      var board = new Board.Board(_user);
      board.PlayGame();
    }

    private void Logout()
    {
      _keepRunning = false;
    }

    private void GetStats()
    {
      _user.GetStats();
    }
  }
}