using Microsoft.AspNetCore.Mvc;
using waterfood.Core.Services.Interfaces;
using waterfood.Data.Entities.Generals;
using waterfood.Data.Entities.Users;

namespace waterfood.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IAdminService _adminService;

        public RoleController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Route("/Admin/Roles")]
        public IActionResult Roles()
        {
            var list = _adminService.GetAllRoles();
            return View(list);
        }
        [Route("/Admin/Role")]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateRole(Role role)
        {
            _adminService.CreateRole(role);
            return View("Roles", _adminService.GetAllRoles());
        }

        [Route("/Admin/Role/UpdateRole/{id}")]
        public IActionResult UpdateRole(int id)
        {
            var role = _adminService.GetRoleById(id);
            return View(role);
        }

        [HttpPost]
        public IActionResult EditRole(Role role)
        {
            role = _adminService.UpdateRole(role);
            return View("Roles", _adminService.GetAllRoles());
        }

        [Route("/Admin/Role/DeleteRole/{id}")]
        public IActionResult DeleteRole(int id)
        {
            var role = _adminService.GetRoleById(id);
            _adminService.DeleteRole(role);

            return View("Roles", _adminService.GetAllRoles());
        }

        [HttpPost]
        public IActionResult Search(string verb)
        {
            return View("Roles", _adminService.GetAllRoles(verb));
        }
    }
}
