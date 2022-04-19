using System.Dynamic;
using api.Interfaces;
using api.Models;

namespace api.Database
{
    public class DepartmentDataHandler : IDepartmentDatahandler
    {
        private Database db { get; set; }

        public DepartmentDataHandler()
        {
            db = new Database();
        }
        public void Delete(int id)
        {
            System.Console.WriteLine("Preparing to delete");

            string stm = $@"DELETE FROM department WHERE department_id = {id}";

            db.Open();
            db.Delete(stm);
            db.Close();

            System.Console.WriteLine("Deletion successful");
        }

        public Dictionary<string, object?> GetValues(Department department)
        {
            var values = new Dictionary<string, object?>(){
                {"@department_id", department.DepartmentID},
                {"@department_name", department.DepartmentName},
            };

            return values;
        }

        public void Insert(Department department)
        {
            System.Console.WriteLine("Preparing to insert");

            //throw new NotImplementedException();

            var departments = GetValues(department);
            string stm = @"INSERT INTO department (department_name) 
                        VALUES (@department_name)";

            db.Open();
            db.Insert(stm, departments);
            db.Close();
            System.Console.WriteLine("Insertion successful");
        }

        public List<Department> Select()
        {
            System.Console.WriteLine("Preparing to select");

            List<Department> allTheDepartments = new List<Department>();

            string stm = @"SELECT * from department";
            System.Console.WriteLine(stm);
            db.Open();
            List<ExpandoObject> results = db.Select(stm);

            foreach (dynamic item in results)
            {
                Department temp = new Department()
                { 
                    DepartmentID = item.department_id,
                    DepartmentName = item.department_name
                };

                allTheDepartments.Add(temp);

            }

            db.Close();

            System.Console.WriteLine("Selection successful");

            return allTheDepartments;
        }

        public List<Department> SelectById(int id)
        {
            System.Console.WriteLine("Preparing to select by id");

            List<Department> theDepartment = new List<Department>();

            string stm = @"SELECT * from department WHERE department_id = " + id + " LIMIT 1";
            db.Open();
            List<ExpandoObject> results = db.Select(stm);


            foreach (dynamic item in results)
            {
                Department temp = new Department()
                {
                    DepartmentID = item.department_id,
                    DepartmentName = item.department_name
                };

                theDepartment.Add(temp);
            }
            db.Close();

            System.Console.WriteLine("Selection successful");

            return theDepartment;
        }

        public List<Department> SelectByDepartmentName(string department)
        {
            throw new NotImplementedException();
        }

        public void Update(Department department)
        {
            System.Console.WriteLine("Preparing to update");

            var values = GetValues(department);

            string stm = @"UPDATE department SET
            department_name = @department_name
            WHERE department_id = @department_id";

            db.Open();
            db.Update(stm, values);
            db.Close();

            System.Console.WriteLine("Updation successful");
        }
    }
}