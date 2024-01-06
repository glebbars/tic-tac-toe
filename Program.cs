namespace tic_tac_toe
{
  class Program
  {
    private readonly static Dictionary<string, string> Users = new Dictionary<string, string>();
    private readonly static Dictionary<string, int> ratings = new Dictionary<string, int>();
    private readonly static List<string> gameHistory = new List<string>();

    private static string currentPlayer;

    private static void Main(string[] args)
    {
      Console.WriteLine("Ласкаво просимо до гри у хрестики-ноліки!");

      while (true)
      {
        Console.WriteLine("\n1. Реєстрація");
        Console.WriteLine("2. Вхід");
        Console.WriteLine("3. Вихід");
        Console.Write("Виберіть опцію: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
          case "1":
            RegisterUser();

            break;
          case "2":
            Login();

            break;
          case "3":
            Environment.Exit(0);

            break;
          default:
            Console.WriteLine("Невірний вибір. Спробуйте ще раз.");

            break;
        }
      }
    }

    private static void RegisterUser()
    {
      Console.Write("Введіть ім'я користувача: ");
      var username = Console.ReadLine();

      if (username != null && !Users.ContainsKey(username))
      {
        Console.Write("Введіть пароль: ");
        var password = Console.ReadLine();

        if (password != null)
        {
          Users.Add(username, password);
        }

        ratings.Add(username, 1000);

        Console.WriteLine("Ви успішно зареєстровані!");
      }
      else
      {
        Console.WriteLine("Користувач з таким ім'ям вже існує. Виберіть інше ім'я.");
      }
    }

    private static void Login()
    {
      Console.Write("Введіть ім'я користувача: ");
      var username = Console.ReadLine();

      if (username != null && Users.ContainsKey(username))
      {
        Console.Write("Введіть пароль: ");
        var password = Console.ReadLine();

        if (Users[username] == password)
        {
          currentPlayer = username;
          Console.WriteLine($"Ви увійшли як {currentPlayer}.");
          PlayGame();
        }
        else
        {
          Console.WriteLine("Неправильний пароль. Спробуйте ще раз.");
        }
      }
      else
      {
        Console.WriteLine("Користувача з таким ім'ям не існує. Будь ласка, зареєструйтесь.");
      }
    }

    private static void PlayGame()
    {
      char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
      var player = 1; // Змінна для визначення гравця (1 або 2)
      int choice; // Вибір позначки на дошці
      var flag = 0; // 1: гра триває, 0: гра закінчена

      do
      {
        Console.Clear(); // Очистити консоль
        Console.WriteLine($"Гравець 1 ({currentPlayer}): X та Гравець 2: O");
        Console.WriteLine("\n");
        if (player % 2 == 0)
        {
          Console.WriteLine("Гравець 2 грає");
        }
        else
        {
          Console.WriteLine($"Гравець 1 ({currentPlayer}) грає");
        }

        Console.WriteLine("\n");
        Board(board);

        // Перевірка чи введений символ є числом і в діапазоні від 1 до 9
        var validInput = false;
        do
        {
          Console.Write("Виберіть номер клітинки (1-9): ");
          var input = Console.ReadLine();
          validInput = int.TryParse(input, out choice);
          if (!validInput || choice < 1 || choice > 9 || board[choice - 1] == 'X' || board[choice - 1] == 'O')
          {
            Console.WriteLine("Введіть дійсне число від 1 до 9 та вільну клітинку.");
          }
        } while (!validInput || choice < 1 || choice > 9 || board[choice - 1] == 'X' || board[choice - 1] == 'O');

        // Перевірка чи введена позначка вільна
        if (player % 2 == 0)
        {
          board[choice - 1] = 'O';
        }
        else
        {
          board[choice - 1] = 'X';
        }

        flag = CheckWin(board);
        player++;
      } while (flag != 1 && flag != -1);

      Console.Clear();
      Board(board);

      if (flag == 1)
      {
        Console.WriteLine($"Гравець {currentPlayer} переміг!");
        RegisterGameResult(true);
      }
      else
      {
        Console.WriteLine("Гра закінчилася в нічию");
        RegisterGameResult(false);
      }

      Console.ReadLine();
    }

    private static void Board(char[] board)
    {
      Console.WriteLine("     |     |      ");
      Console.WriteLine($"  {board[0]}  |  {board[1]}  |  {board[2]}");
      Console.WriteLine("_____|_____|_____ ");
      Console.WriteLine("     |     |      ");
      Console.WriteLine($"  {board[3]}  |  {board[4]}  |  {board[5]}");
      Console.WriteLine("_____|_____|_____ ");
      Console.WriteLine("     |     |      ");
      Console.WriteLine($"  {board[6]}  |  {board[7]}  |  {board[8]}");
      Console.WriteLine("     |     |      ");
    }

    private static int CheckWin(char[] board)
    {
      // Логіка перевірки переможця тут

      // Повертає 1, якщо є переможець
      // Повертає -1, якщо нічия
      // Повертає 0, якщо гра триває

      #region Horzontal Winning Condtion

      // Winning Condition For First Row
      if (board[0] == board[1] && board[1] == board[2])
      {
        return 1;
      }
      // Winning Condition For Second Row

      if (board[3] == board[4] && board[4] == board[5])
      {
        return 1;
      }
      // Winning Condition For Third Row

      if (board[6] == board[7] && board[7] == board[8])
      {
        return 1;
      }

      #endregion

      #region Vertical Winning Condtion

      // Winning Condition For First Column

      if (board[0] == board[3] && board[3] == board[6])
      {
        return 1;
      }
      // Winning Condition For Second Column

      if (board[1] == board[4] && board[4] == board[7])
      {
        return 1;
      }
      // Winning Condition For Third Column

      if (board[2] == board[5] && board[5] == board[8])
      {
        return 1;
      }

      #endregion

      #region Diagonal Winning Condtion

      // Winning Condition For First Diagonal

      if (board[0] == board[4] && board[4] == board[8])
      {
        return 1;
      }
      // Winning Condition For Second Diagonal

      if (board[2] == board[4] && board[4] == board[6])
      {
        return 1;
      }

      #endregion

      #region Check For Draw

      // If all the cells or values filled with X or O then any player has won

      if (board[0] != '1' && board[1] != '2' && board[2] != '3' && board[3] != '4' &&
          board[4] != '5' && board[5] != '6' && board[6] != '7' && board[7] != '8' && board[8] != '9')
      {
        return -1;
      }

      #endregion

      return 0;
    }

    private static void RegisterGameResult(bool isWinner)
    {
      var ratingChange = isWinner ? 10 : -5;
      ratings[currentPlayer] += ratingChange;

      var result = isWinner ? "перемога" : "поразка";
      var gameRecord = $"{currentPlayer}: {result}";
      gameHistory.Add(gameRecord);

      Console.WriteLine($"Ваш рейтинг: {ratings[currentPlayer]}");
    }
  }
}