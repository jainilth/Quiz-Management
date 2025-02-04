using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Quiz_Management.Controllers
{
    public class QuestionLevelController : Controller
    {
        private IConfiguration configuration;

        public QuestionLevelController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }


        public IActionResult QuestionLevelList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_QuestionLevel_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        public IActionResult AddQuestionLevel()
        {
            return View();
        }
    }
}
