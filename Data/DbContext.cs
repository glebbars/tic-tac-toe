namespace tic_tac_toe.Data
{
  public class DbContext
  {
    public List<User.User> Users { get; } = new List<User.User>();
  }
}