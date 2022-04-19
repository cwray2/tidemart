using api.Models;

namespace api.Interfaces
{
    public interface IDepartmentDatahandler
    {
         public List<Department> Select();
         public List<Department> SelectById(int id);
         public List<Department> SelectByDepartmentName(string department);
         public void Delete(int id);
         public void Insert(Department department);
         public void Update(Department department);

         public Dictionary<string, object?> GetValues(Department department);
    }
}