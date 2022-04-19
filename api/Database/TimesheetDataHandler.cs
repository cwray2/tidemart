using System.Dynamic;
using api.Interfaces;
using api.Models;

namespace api.Database
{
    public class TimesheetDataHandler : ITimesheetDataHandler
    {
        private Database db { get; set; }

        public TimesheetDataHandler()
        {
            db = new Database();
        }
        public void Delete(int id)
        {
            System.Console.WriteLine("Preparing to delete");

            string stm = $@"DELETE FROM timesheet WHERE timesheet_id = {id}";

            db.Open();
            db.Delete(stm);
            db.Close();

            System.Console.WriteLine("Deletion successful");
        }

        public Dictionary<string, object?> GetValues(Timesheet timesheet)
        {
            var values = new Dictionary<string, object?>(){
                {"@timesheet_id", timesheet.TimesheetID},
                {"@start_time", timesheet.StartTime},
                {"@end_time", timesheet.EndTime},
                {"@description", timesheet.Description},
                {"@lunch", timesheet.Lunch},
                {"@user_id", timesheet.UserID},
            };

            return values;
        }

        public void Insert(Timesheet timesheet)
        {
            System.Console.WriteLine("Preparing to insert");

            //throw new NotImplementedException();

            var timesheets = GetValues(timesheet);
            string stm = @"INSERT INTO timesheet (start_time, end_time, description, lunch, user_id) 
                        VALUES (@start_time, @end_time, @description, @lunch, @user_id)";

            db.Open();
            db.Insert(stm, timesheets);
            db.Close();
            System.Console.WriteLine("Insertion successful");
        }

        public List<Timesheet> Select()
        {
            System.Console.WriteLine("Preparing to select");

            List<Timesheet> allTheTimesheets = new List<Timesheet>();

            string stm = @"SELECT * from timesheet";
            System.Console.WriteLine(stm);
            db.Open();
            List<ExpandoObject> results = db.Select(stm);

            foreach (dynamic item in results)
            {
                Timesheet temp = new Timesheet()
                { 
                    TimesheetID = item.timesheet_id,
                    StartTime = item.start_time,
                    EndTime = item.end_time,
                    Description = item.description,
                    Lunch = item.lunch,
                    UserID = item.user_id
                };

                allTheTimesheets.Add(temp);

            }

            db.Close();

            System.Console.WriteLine("Selection successful");

            return allTheTimesheets;
        }

        public List<Timesheet> SelectById(int id)
        {
            System.Console.WriteLine("Preparing to select");

            List<Timesheet> theTimesheet = new List<Timesheet>();

            string stm = @"SELECT * from timesheet WHERE timesheet_id = " + id + " LIMIT 1";
            System.Console.WriteLine(stm);
            db.Open();
            List<ExpandoObject> results = db.Select(stm);

            foreach (dynamic item in results)
            {
                Timesheet temp = new Timesheet()
                { 
                    TimesheetID = item.timesheet_id,
                    StartTime = item.start_time,
                    EndTime = item.end_time,
                    Description = item.description,
                    Lunch = item.lunch,
                    UserID = item.user_id
                };

                theTimesheet.Add(temp);

            }

            db.Close();

            System.Console.WriteLine("Selection successful");

            return theTimesheet;
        }

        public List<Timesheet> SelectByUsername(string timesheet)
        {
            throw new NotImplementedException();
        }

        public void Update(Timesheet timesheet)
        {
            System.Console.WriteLine("Preparing to update");

            var values = GetValues(timesheet);

            string stm = @"UPDATE timesheet SET
            start_time = @start_time,
            end_time = @end_time,
            description = @description,
            lunch = @lunch,
            user_id = @user_id
            WHERE timesheet_id = @timesheet_id";

            db.Open();
            db.Update(stm, values);
            db.Close();

            System.Console.WriteLine("Updation successful");
        }
    }
}