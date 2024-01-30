using Microsoft.AspNetCore.Mvc;
using waterfood.Core.Services.Interfaces;
using waterfood.Data.Entities.Items;
using waterfood.Data.Entities.Users;

namespace waterfood.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IAdminService _adminService;

        public UserController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [Route("/Admin/Users")]
        public IActionResult Users()
        {
            var list = _adminService.GetAllUsers();
            return View(list);
        }

        [Route("/Admin/User")]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            _adminService.CreateUser(user);
            return View("Users", _adminService.GetAllUsers());
        }

        [Route("/Admin/User/UpdateUser/{id}")]
        public IActionResult UpdateUser(int id)
        {
            var user = _adminService.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
            user = _adminService.UpdateUser(user);
            return View("Users", _adminService.GetAllUsers());
        }

        [Route("/Admin/User/DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _adminService.GetItemById(id);
            _adminService.DeleteItem(user);

            return View("Users", _adminService.GetAllUsers());
        }

        [HttpPost]
        public IActionResult Search(string verb)
        {
            return View("Users", _adminService.GetAllUsers(verb));
        }
    }
}
