using Microsoft.AspNetCore.Mvc;

namespace Quiz_Management.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult QuestionList()
        {
            return View();
        }
        public IActionResult AddQuestion()
        {
            return View();
        }
    }
}
