namespace tic_tac_toe.Utils
{
  public static class ConsoleHelper
  {
    public static void ShowMenu(Func<bool> shouldContinue, Dictionary<int, (string commandInfo, Action command)> uiCommands)
    {
      while (shouldContinue())
      {
        PrintMenuOptions(uiCommands);

        var choice = Console.ReadLine();

        if (int.TryParse(choice, out var optionToChoose) && uiCommands.ContainsKey(optionToChoose))
        {
          uiCommands[optionToChoose].command();
        }
        else
        {
          Console.WriteLine("\nНевірний вибір. Будь ласка оберіть іншу опцію");
        }
      }
    }

    private static void PrintMenuOptions(Dictionary<int, (string commandInfo, Action command)> uiCommands)
    {
      foreach (var (optionToPrint, (commandInfo, _)) in uiCommands)
        Console.WriteLine($"{optionToPrint}. {commandInfo}");

      Console.Write("\nОберіть опцію: ");
    }
  }

}