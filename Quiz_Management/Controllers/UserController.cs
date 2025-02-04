using Microsoft.AspNetCore.Mvc;

namespace Quiz_Management.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserList()
        {
            return View();
        }
        public IActionResult AddUsers()
        {
            return View();
        }
    }
}
