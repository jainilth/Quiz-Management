using System.Data;
using System.Data.SqlClient;
using Quiz_Management.Models;
using Microsoft.AspNetCore.Mvc;


namespace Quiz_Management.Controllers
{
    public class UserController : Controller
    {
        private IConfiguration configuration;
        public UserController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public IActionResult UserList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_MST_User_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);
            return View(dataTable);
        }
        public IActionResult UserSave(UserModel model)
        {
            if(ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                if (model.UserID == 0)
                {
                    command.CommandText = "PR_MST_User_Insert";
                }
                else
                {
                    command.CommandText = "PR_MST_User_Update";
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                }
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = model.UserName;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = model.Email;
                command.Parameters.Add("@Password", SqlDbType.VarChar).Value = model.Password;
                command.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = model.Mobile;
                command.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = model.IsAdmin;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                command.ExecuteNonQuery();
                return RedirectToAction("UserList");
            }
            return View("AddEditUsers",model);
        }

        public IActionResult DeleteUser(int UserID)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "[dbo].[PR_MST_User_Delete]";
            sqlCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
            sqlCommand.ExecuteNonQuery();
            return RedirectToAction("UserList");
        }
        public IActionResult AddEditUsers(int UserID)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_User_SelectByID";
            command.Parameters.AddWithValue("@UserID", UserID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            UserModel model = new UserModel();
            foreach (DataRow row in dataTable.Rows)
            {
                model.UserID = Convert.ToInt32(@row["UserID"]);
                model.UserName = @row["UserName"].ToString();
                model.Email = @row["Email"].ToString();
                model.Password = @row["Password"].ToString();
                model.Mobile = @row["Mobile"].ToString();
                model.IsActive = Convert.ToBoolean(@row["IsActive"]);
                model.IsAdmin = Convert.ToBoolean(@row["IsAdmin"]);
            }
            return View("AddEditUsers",model);
        }
    }
}
