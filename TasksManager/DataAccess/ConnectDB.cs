using System.Data.SqlClient;
using TasksManager.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace TasksManager.DataAccess
{
    public class ConnectDB: IConnectDB
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public ConnectDB(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("TaskDatabase");
        }
        public List<Taskitem> ReadTasks()
        {
            List<Taskitem> results = new List<Taskitem>();
            string queryString = "SELECT *  FROM  [dbo].[AllTasks]";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Taskitem item = new Taskitem() 
                        { 
                            Id = reader.GetInt32("Id"),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            DueDate = DateTime.Parse(reader["DueDate"].ToString()),
                            StartDate = DateTime.Parse(reader["StartDate"].ToString()),
                            EndDate = DateTime.Parse(reader["EndDate"].ToString()),
                            Priority = (PriorityEnum) int.Parse(reader["Priority"].ToString()),
                            Status = (StatusEnum)int.Parse(reader["Status"].ToString())
                        };

                        results.Add(item);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return results;
        }
        public void InsertTask(Taskitem taskitem)
        {
            RunQuery(string.Format("INSERT INTO AllTasks (Name, Description, DueDate, StartDate, EndDate, Priority, Status) VALUES ('{0}','{1}','{2}','{3}','{4}',{5},{6})",
                                                    taskitem.Name, taskitem.Description, taskitem.DueDate, taskitem.StartDate, taskitem.EndDate, (int)taskitem.Priority, (int)taskitem.Status));
        }
        public void UpdateTask(Taskitem taskitem)
        {
            RunQuery(string.Format("UPDATE AllTasks SET Name = '{0}', Description = '{1}', DueDate = '{2}', StartDate = '{3}', EndDate = '{4}', Priority = {5}, Status = {6}  WHERE ID = {7}", 
                                               taskitem.Name, taskitem.Description, taskitem.DueDate, taskitem.StartDate, taskitem.EndDate, (int)taskitem.Priority, (int)taskitem.Status, taskitem.Id));
        }
        private void RunQuery(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = queryString;
                    cmd.Connection = connection;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex) { }
            }
        }
    }
}
