using api.Models;

namespace api.Interfaces
{
    public interface IDepartmentDatahandler
    {
         public List<User> Select();
         public List<User> SelectById(int id);
         public List<User> SelectByUsername(string department);
         public void Delete(int id);
         public void Insert(User User);
         public void Update(User User);

         public Dictionary<string, object?> GetValues(User User);
    }
}