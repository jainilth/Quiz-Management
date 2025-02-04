using Microsoft.AspNetCore.Mvc;

namespace Quiz_Management.Controllers
{
    public class QuizWiseQuestionController : Controller
    {
        public IActionResult QuizWiseQuestionList()
        {
            return View();
        }
        public IActionResult AddQuizWiseQuestion()
        {
            return View();
        }
    }
}
