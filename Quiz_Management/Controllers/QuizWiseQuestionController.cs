﻿using System.Data;
using System.Data.SqlClient;
using System.Windows.Input;
using Microsoft.AspNetCore.Mvc;
using Quiz_Management.Models;

namespace Quiz_Management.Controllers
{
    [CheckAccess]
    public class QuizWiseQuestionController : Controller
    {
        private IConfiguration configuration;

        public QuizWiseQuestionController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        #region Quiz Wise Question List
        public IActionResult QuizWiseQuestionList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection=new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_QuizWiseQuestions_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        #endregion

        #region Quiz wise Question Save
        public IActionResult QuizWiseQuestionSave(QuizWiseQuestionModel model)
        {
            int UserID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            if (ModelState.IsValid)
            {
                QuizDropDown();
                QuestionDropDown();
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                if (model.QuizWiseQuestionsID == 0)
                {
                    command.CommandText = "PR_MST_QuizWiseQuestions_Insert";
                }
                else
                {
                    command.CommandText = "PR_MST_QuizWiseQuestions_Update";
                    command.Parameters.Add("@QuizWiseQuestionsID", SqlDbType.Int).Value = model.QuizWiseQuestionsID;
                }
                command.Parameters.Add("@QuizID", SqlDbType.Int).Value = model.QuizID;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                command.Parameters.Add("@QuestionID", SqlDbType.Int).Value = model.QuestionID;
                command.ExecuteNonQuery();

                return RedirectToAction("QuizWiseQuestionList");
            }
            else
            {
                QuizDropDown();
                QuestionDropDown();
                return View("AddEditQuizWiseQuestion", model);
            }
        }
        #endregion

        #region Add Edit Quiz Wise Question
        public IActionResult AddEditQuizWiseQuestion(int QuizWiseQuestionsID)
        {
            QuizDropDown();
            QuestionDropDown();
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_QuizWiseQuestions_SelectByID";
            command.Parameters.AddWithValue("@QuizWiseQuestionsID", QuizWiseQuestionsID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            QuizWiseQuestionModel model = new QuizWiseQuestionModel();
            foreach(DataRow row in dataTable.Rows)
            {
                model.QuizID = Convert.ToInt32(@row["QuizID"]);
                model.QuestionID = Convert.ToInt32(@row["QuestionID"]);
                model.UserID = Convert.ToInt32(@row["UserID"]);
            }
            return View("AddEditQuizWiseQuestion",model);
        }
        #endregion

        #region Quiz Wise Question Delete
        public IActionResult QuizWiseQuestionDelete(int QuizWiseQuestionsID)
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_QuizWiseQuestions_Delete";
            command.Parameters.AddWithValue("@QuizWiseQuestionsID", QuizWiseQuestionsID);
            command.ExecuteNonQuery();
            return RedirectToAction("QuizWiseQuestionList");
        }
        #endregion 

        #region Question DropDown
        public void QuestionDropDown()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Dropdown_MST_Question";
            SqlDataReader reader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            List<QuestionDropdownModel> list = new List<QuestionDropdownModel>();
            foreach (DataRow data in dataTable.Rows)
            {
                QuestionDropdownModel model = new QuestionDropdownModel();
                model.QuestionID = Convert.ToInt32(data["QuestionID"]);
                model.QuestionText = data["QuestionText"].ToString();
                list.Add(model);
            }
            ViewBag.Question = list;
        }
        #endregion

        #region Quiz DropDown
        public void QuizDropDown()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Dropdown_MST_Quiz";
            SqlDataReader reader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            List<QuizDropdownModel> list = new List<QuizDropdownModel>();
            foreach (DataRow data in dataTable.Rows)
            {
                QuizDropdownModel model = new QuizDropdownModel();
                model.QuizID = Convert.ToInt32(data["QuizID"]);
                model.QuizName = data["QuizName"].ToString();
                list.Add(model);
            }
            ViewBag.Quiz = list;
        }
        #endregion
    }
}
