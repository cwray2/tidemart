using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;

//If using MySQL <dotnet add package MySql.Data --version 8.0.28> format is "MySqlConnection, ..."
using MySql.Data.MySqlClient;

//If using SQL Server <dotnet add package System.Data.SqlClient --version 4.8.3> format is "SqlConnection, SqlCommand,..."
//using System.Data.SqlClient;

namespace api.Database
{
    public class Database
    {
        public string cs { get; set; }
        public MySqlConnection Conn { get; set; }

        public Database()
        {
            //go to edit system enviroment variables on specific machine
            string? server = "lfmerukkeiac5y5w.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";

            string? name = "ag1hw62z4b9v0jda";

            string? port = "3306";

            string? userName = "e92azxwf0oh0khua";

            string? password = "sywc0wkzbmznbks5";

            System.Console.WriteLine("got the database " + server);

            this.cs = $@"server = {server};userName = {userName};database = {name};port = {port}; password={password}; Convert Zero Datetime={true}";
            this.Conn = new MySqlConnection(this.cs);
        }

        public void Open()
        {
            if (Conn.State != ConnectionState.Open)
            {
                this.Conn.Open();
            }
        }

        public void Close()
        {
            this.Conn.Close();
        }

        public List<ExpandoObject> Select(string query)
        {
            List<ExpandoObject> results = new();
            try
            {
                using var cmd = new MySqlCommand(query, this.Conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var temp = new ExpandoObject() as IDictionary<string, Object?>;
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {

                        temp.TryAdd(rdr.GetName(i), rdr.GetValue(i) != DBNull.Value ? rdr.GetValue(i) : null);
                    }

                    results.Add((ExpandoObject)temp);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Select Query Error");
                System.Console.WriteLine(e.Message);
                throw new Exception();
            }

            return results;
        }


        public void Insert(string query, Dictionary<string, object?> values)
        {
            QueryWithData(query, values);
        }

        public void Update(string query, Dictionary<string, object?> values)
        {
            QueryWithData(query, values);
        }

        public void Delete(string stm)
        {
            using var cmd = new MySqlCommand(stm, this.Conn);
            cmd.ExecuteNonQuery();
        }


        private void QueryWithData(string query, Dictionary<string, object?> values)
        {
            try
            {
                using var cmd = new MySqlCommand(query, this.Conn);
                foreach (var p in values)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }

                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Error Inserting Data");
                System.Console.WriteLine(e.Message);
                throw(e);
               
            }
        }
    }
}