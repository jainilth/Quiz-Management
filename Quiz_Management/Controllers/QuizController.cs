using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Quiz_Management.Models;
using System.Reflection;

namespace Quiz.Controllers
{
    public class QuizController : Controller
    {
        private IConfiguration configuration;
        public QuizController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public IActionResult QuizList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_Quiz_SelectAll";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        return View(table);
                    }
                }
            }
        }

        public IActionResult QuizSave(QuizModel model)
        {
            if(ModelState.IsValid)
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                if (model.QuizID == 0)
                {
                    command.CommandText = "PR_MST_Quiz_Insert";
                }
                else
                {
                    command.CommandText = "PR_MST_Quiz_Update";
                    command.Parameters.Add("@QuizID", SqlDbType.Int).Value = model.QuizID;
                }
                command.Parameters.Add("@QuizName", SqlDbType.VarChar).Value = model.QuizName;
                command.Parameters.Add("@TotalQuestions", SqlDbType.Int).Value = model.TotalQuestions;
                command.Parameters.Add("@QuizDate", SqlDbType.DateTime).Value = model.QuizDate;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                command.ExecuteNonQuery();
                return RedirectToAction("QuizList");
            }
            return View("QuizAddEdit",model);
        }

        public IActionResult QuizAddEdit(int QuizID)
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection Connection=new SqlConnection(connectionString);
            Connection.Open();
            SqlCommand command = Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Quiz_SelectByID";
            command.Parameters.AddWithValue("@QuizID", QuizID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable datatable = new DataTable();
            datatable.Load(reader);
            QuizModel model = new QuizModel();
            foreach (DataRow row in datatable.Rows)
            {
                model.QuizName = @row["QuizName"].ToString();
                model.TotalQuestions = Convert.ToInt32(@row["TotalQuestions"]);
                model.QuizDate = Convert.ToDateTime(@row["QuizDate"]);
                model.UserID = Convert.ToInt32(@row["UserID"]);
            }
            return View("QuizAddEdit", model);
        }
        public IActionResult DeleteQuiz(int QuizID)
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand Command = connection.CreateCommand();
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "[dbo].[PR_MST_Quiz_Delete]";
            Command.Parameters.Add("@QuizID",SqlDbType.Int).Value=QuizID;
            Command.ExecuteNonQuery();
            return RedirectToAction("QuizList");
        }
    }
}
