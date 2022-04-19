using api.Models;

namespace api.Interfaces
{
    public interface ITimesheetDataHandler
    {
         public List<Timesheet> Select();
         public List<Timesheet> SelectById(int id);
         public List<Timesheet> SelectByUsername(string timesheet);
         public void Delete(int id);
         public void Insert(Timesheet timesheet);
         public void Update(Timesheet timesheet);

         public Dictionary<string, object?> GetValues(Timesheet timesheet);
    }
}