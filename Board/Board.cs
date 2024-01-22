using tic_tac_toe.Game;

namespace tic_tac_toe.Board
{
  public class Board
  {

    private readonly User.User _currentUser;
    private readonly char[] _gameBoard = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    public Board(User.User currentUser)
    {
      _currentUser = currentUser;
    }

    private void RegisterResult(GameStatus gameStatus, bool isUserMove)
    {
      if (gameStatus == GameStatus.FinishedWithDraw)
      {
        Console.WriteLine("Гра закінчилася в нічию");
        _currentUser.DrawGame(new Game.Game(Result.Draw, 100));

        return;
      }

      if (isUserMove)
      {
        Console.WriteLine($"Гравець {_currentUser.Name} переміг!");
        _currentUser.WinGame(new Game.Game(Result.Win, 100));
      }
      else
      {
        Console.WriteLine($"Гравець {_currentUser.Name} програв!");
        _currentUser.LoseGame(new Game.Game(Result.Lose, 100));
      }
    }

    private bool IsChoiceOutOfRange(int choice)
    {
      return choice < 1 || choice > 9;
    }

    private bool IsChoiceAlreadyTaken(int choice)
    {
      return _gameBoard[choice - 1] == '\u2705' || _gameBoard[choice - 1] == '\u274c';
    }

    public void PlayGame()
    {
      GameStatus gameStatus; // 1: перемога, -1: нічия; 0: гра триває
      var isUsersMove = true; // Змінна для визначення гравця (1 або 2)

      do
      {
        Console.Clear(); // Очистити консоль
        Console.WriteLine($"Гравець 1 ({_currentUser.Name}): X та Гравець 2: O\n");
        Console.WriteLine(isUsersMove ? $"Гравець 1 ({_currentUser.Name}) грає" : "Гравець 2 грає\n");

        DrawBoard();

        bool validInput; // Перевірка чи введений символ є числом і в діапазоні від 1 до 9
        int markNumber; // Вибір позначки на дошці
        do
        {
          Console.Write("Виберіть номер клітинки (1-9): ");
          var input = Console.ReadLine();
          validInput = int.TryParse(input, out markNumber);

          if (!validInput)
          {
            Console.WriteLine("Введіть дійсне число.");
          }
          else if (IsChoiceOutOfRange(markNumber))
          {
            Console.WriteLine("Введіть число від 1 до 9.");
          }
          else if (IsChoiceAlreadyTaken(markNumber))
          {
            Console.WriteLine("Оберіть вільну клітинку.");
          }
        } while (!validInput || IsChoiceOutOfRange(markNumber) || IsChoiceAlreadyTaken(markNumber));

        _gameBoard[markNumber - 1] = isUsersMove ? '\u2705' : '\u274c';

        gameStatus = CheckGameStatus();

        // switch players only if game continues
        if (gameStatus == GameStatus.InProgress)
        {
          isUsersMove = !isUsersMove;
        }
      } while (gameStatus == GameStatus.InProgress);

      Console.Clear();
      DrawBoard();
      RegisterResult(gameStatus, isUsersMove);

      Console.ReadLine();
    }


    private void DrawBoard()
    {
      Console.WriteLine("     |     |      ");
      Console.WriteLine($"  {_gameBoard[0]}  |  {_gameBoard[1]}  |  {_gameBoard[2]}  ");
      Console.WriteLine("_____|_____|_____ ");
      Console.WriteLine("     |     |      ");
      Console.WriteLine($"  {_gameBoard[3]}  |  {_gameBoard[4]}  |  {_gameBoard[5]}  ");
      Console.WriteLine("_____|_____|_____ ");
      Console.WriteLine("     |     |      ");
      Console.WriteLine($"  {_gameBoard[6]}  |  {_gameBoard[7]}  |  {_gameBoard[8]}  ");
      Console.WriteLine("     |     |      ");
    }

    // Логіка перевірки переможця тут
    // Повертає 1, якщо є переможець
    // Повертає -1, якщо нічия
    // Повертає 0, якщо гра триває
    private GameStatus CheckGameStatus()
    {
      #region Horzontal Winning Condtion

      // Winning Condition For First Row
      if (_gameBoard[0] == _gameBoard[1] && _gameBoard[1] == _gameBoard[2])
      {
        return GameStatus.FinishedWithVictory;
      }

      // Winning Condition For Second Row
      if (_gameBoard[3] == _gameBoard[4] && _gameBoard[4] == _gameBoard[5])
      {
        return GameStatus.FinishedWithVictory;
      }

      // Winning Condition For Third Row
      if (_gameBoard[6] == _gameBoard[7] && _gameBoard[7] == _gameBoard[8])
      {
        return GameStatus.FinishedWithVictory;
      }

      #endregion

      #region Vertical Winning Condtion

      // Winning Condition For First Column
      if (_gameBoard[0] == _gameBoard[3] && _gameBoard[3] == _gameBoard[6])
      {
        return GameStatus.FinishedWithVictory;
      }

      // Winning Condition For Second Column
      if (_gameBoard[1] == _gameBoard[4] && _gameBoard[4] == _gameBoard[7])
      {
        return GameStatus.FinishedWithVictory;
      }

      // Winning Condition For Third Column
      if (_gameBoard[2] == _gameBoard[5] && _gameBoard[5] == _gameBoard[8])
      {
        return GameStatus.FinishedWithVictory;
      }

      #endregion

      #region Diagonal Winning Condtion

      // Winning Condition For First Diagonal
      if (_gameBoard[0] == _gameBoard[4] && _gameBoard[4] == _gameBoard[8])
      {
        return GameStatus.FinishedWithVictory;
      }

      // Winning Condition For Second Diagonal
      if (_gameBoard[2] == _gameBoard[4] && _gameBoard[4] == _gameBoard[6])
      {
        return GameStatus.FinishedWithVictory;
      }

      #endregion

      #region Check For Draw

      // If all the cells or values filled with X or O then any player has won
      if (_gameBoard[0] != '1' && _gameBoard[1] != '2' && _gameBoard[2] != '3' && _gameBoard[3] != '4' &&
          _gameBoard[4] != '5' && _gameBoard[5] != '6' && _gameBoard[6] != '7' && _gameBoard[7] != '8' && _gameBoard[8] != '9')
      {
        return GameStatus.FinishedWithDraw;
      }

      #endregion

      return GameStatus.InProgress;
    }

    private enum GameStatus
    {
      InProgress,
      FinishedWithVictory,
      FinishedWithDraw
    }
  }
}