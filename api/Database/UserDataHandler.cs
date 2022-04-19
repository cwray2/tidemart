using System.Runtime.CompilerServices;
using System.Data.Common;
using System.Data;
using System.Dynamic;
using api.Interfaces;
using api.Models;

namespace api.Database
{
    public class UserDataHandler : IUserDataHandler
    {
        private Database db { get; set; }

        public UserDataHandler()
        {
            db = new Database();
        }
        public void Delete(int id)
        {
            System.Console.WriteLine("Preparing to delete");

            string stm = $@"DELETE FROM user WHERE user_id = {id}";

            db.Open();
            db.Delete(stm);
            db.Close();

            System.Console.WriteLine("Deletion successful");
        }

        public Dictionary<string, object?> GetValues(User User)
        {
            var values = new Dictionary<string, object?>(){
                {"@user_id", User.UserID},
                {"@hash_password", User.Password},
                {"@user_ssn", User.SSN},
                {"@user_first_name", User.FirstName},
                {"@user_middle_name", User.MiddleName},
                {"@user_last_name", User.LastName},
                {"@user_email", User.Email},
                {"@user_start_date", User.StartDate},
                {"@user_salary", User.Salary},
                {"@user_shift_start", User.ShiftStart},
                {"@user_bday", User.Birthday},
                {"@user_phone", User.Phone},
                {"@image_url", User.URL},
                {"@admin_status", User.Admin},
                {"@username", User.Username},
                {"@department_id", User.DepartmentID},
            };

            return values;
        }

        public void Insert(User User)
        {
            System.Console.WriteLine("Preparing to insert");

            //throw new NotImplementedException();

            var users = GetValues(User);
            string stm = @"INSERT INTO user (hash_password, user_ssn, user_first_name, user_middle_name, user_last_name, user_email,
                        user_shift_start, user_bday, user_phone, image_url, admin_status, username, department_id) 
                        VALUES (@hash_password, @user_ssn, @user_first_name, @user_middle_name, @user_last_name, @user_email, @user_shift_start, @user_bday, @user_phone, @image_url, @admin_status, @username, @department_id)";

            db.Open();
            db.Insert(stm, users);
            db.Close();
            System.Console.WriteLine("Insertion successful");
        }

        public List<User> Select()
        {
            System.Console.WriteLine("Preparing to select");

            List<User> allTheUsers = new List<User>();

            string stm = @"SELECT * from user";
            System.Console.WriteLine(stm);
            db.Open();
            List<ExpandoObject> results = db.Select(stm);

            foreach (dynamic item in results)
            {
                User temp = new User()
                { 
                    UserID = item.user_id,
                    Password = item.hash_password,
                    SSN = item.user_ssn,
                    FirstName = item.user_first_name,
                    MiddleName = item.user_middle_name,
                    LastName = item.user_last_name,
                    Email = item.user_email,
                    StartDate = item.user_start_date,
                    Salary = item.user_salary,
                    ShiftStart = item.user_shift_start,
                    Birthday = item.user_bday,
                    Phone = item.user_phone,
                    URL = item.image_url,
                    Admin = item.admin_status,
                    Username = item.username,
                    DepartmentID = item.department_id,
                };

                allTheUsers.Add(temp);

            }

            db.Close();

            System.Console.WriteLine("Selection complete");

            return allTheUsers;
        }

        public List<User> SelectById(int id)
        {
            System.Console.WriteLine("Preparing to select");

            List<User> theUser = new List<User>();

            string stm = @"SELECT * from user WHERE user_id = " + id + " LIMIT 1";
            db.Open();
            List<ExpandoObject> results = db.Select(stm);


            foreach (dynamic item in results)
            {
                User temp = new User()
                {
                    UserID = item.user_id,
                    Password = item.hash_password,
                    SSN = item.user_ssn,
                    FirstName = item.user_first_name,
                    MiddleName = item.user_middle_name,
                    LastName = item.user_last_name,
                    Email = item.user_email,
                    StartDate = item.user_start_date,
                    Salary = item.user_salary,
                    ShiftStart = item.user_shift_start,
                    Birthday = item.user_bday,
                    Phone = item.user_phone,
                    URL = item.image_url,
                    Admin = item.admin_status,
                    Username = item.username,
                    DepartmentID = item.department_id
                };

                theUser.Add(temp);
            }
            db.Close();

            System.Console.WriteLine("Selection successful");

            return theUser;
        }

        public List<User> SelectByUsername(string username)
        {
            List<User> theUser = new List<User>();

            string stm = @"SELECT * from user WHERE username = " + username + " LIMIT 1";
            db.Open();
            List<ExpandoObject> results = db.Select(stm);


            foreach (dynamic item in results)
            {
                User temp = new User()
                {
                    UserID = item.user_id,
                    Password = item.hash_password,
                    SSN = item.user_ssn,
                    FirstName = item.user_first_name,
                    MiddleName = item.user_middle_name,
                    LastName = item.user_last_name,
                    Email = item.user_email,
                    StartDate = item.user_start_date,
                    Salary = item.user_salary,
                    ShiftStart = item.user_shift_start,
                    Birthday = item.user_bday,
                    Phone = item.user_phone,
                    URL = item.image_url,
                    Admin = item.admin_status,
                    Username = item.username,
                    DepartmentID = item.department_id
                };

                theUser.Add(temp);
            }
            db.Close();
            return theUser;
        }

        public void Update(User User)
        {
            System.Console.WriteLine("Preparing to update");

            var values = GetValues(User);

            string stm = @"UPDATE user SET
            hash_password = @hash_password,
            user_ssn = @user_ssn,
            user_first_name = @user_first_name,
            user_middle_name = @user_middle_name,
            user_last_name = @user_last_name,
            user_email = @user_email,
            user_start_date = @user_start_date,
            user_salary = @user_salary,
            user_shift_start = @user_shift_start,
            user_bday = @user_bday,
            user_phone = @user_phone,
            image_url = @image_url,
            admin_status = @admin_status,
            username = @username,
            department_id = @department_id
            WHERE user_id = @user_id";

            db.Open();
            db.Update(stm, values);
            db.Close();

            System.Console.WriteLine("Updation successful");
        }
    }
}