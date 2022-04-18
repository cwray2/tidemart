using api.Models;

namespace api.Interfaces
{
    public interface IUserDataHandler
    {
         public List<User> Select();
         public List<User> SelectById(int id);
         public List<User> SelectByUsername(string username);
         public void Delete(int id);
         public void Insert(User User);
         public void Update(User User);

         public Dictionary<string, object?> GetValues(User User);
    }
}