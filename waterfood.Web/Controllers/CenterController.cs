using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Reflection.Metadata;
using waterfood.Core.Services.Interfaces;
using waterfood.Data.Entities.Centers;
using waterfood.Data.Entities.Users;

namespace waterfood.Web.Controllers
{
    public class CenterController : Controller
    {
        private readonly IAdminService _adminService;

        public CenterController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Route("/Admin/Centers")]
        public IActionResult Centers()
        {
            var list = _adminService.GetAllCenters();
            return View(list);
        }

        [Route("/Admin/Center")]
        public IActionResult AddCenter()
        {
            ViewBag.users = _adminService.GetAllUsers();
            return View();
        }

        [HttpPost]
        public IActionResult CreateCenter(Center center)
        {
            _adminService.CreateCenter(center);
            return View("Centers", _adminService.GetAllCenters());
        }

        [Route("/Admin/Center/UpdateCenter/{id}")]
        public IActionResult UpdateCenter(int id)
        {
            ViewBag.users = _adminService.GetAllUsers();
            var center = _adminService.GetCenterById(id);
            return View(center);
        }

        [HttpPost]
        public IActionResult EditCenter(Center center)
        {
            center = _adminService.UpdateCenter(center);
            return View("Centers", _adminService.GetAllCenters());
        }
        
        [Route("/Admin/Center/DeleteCenter/{id}")]
        public IActionResult DeleteCenter(int id)
        {
            var center = _adminService.GetCenterById(id);
            _adminService.DeleteCenter(center);

            return View("Centers", _adminService.GetAllCenters());
        }

        [HttpPost]
        public IActionResult Search(string verb)
        {
            return View("Centers", _adminService.GetAllCenters(verb));
        }

        [HttpPost]
        public IActionResult SaveCenterImage(IFormFile file)
        {

            _adminService.SetCenterImage(file);
            return View();
            

        }

        [HttpPost]
        public IActionResult Save(IFormFile file,Center center)
        {
            try
            {
                var model = _adminService.SaveNewCenter(file,center,User.Identity?.Name);
                if (model.CenterId > 0)
                {
                    return Redirect("/admin/center/" + model.CenterId + "?error=false&message=success");
                }


                return View("centers", model);
            }
            catch (Exception e)
            {
                if (center.CenterId > 0)
                {
                    return Redirect("/admin/center/" + center.CenterId + "?error=true&message=" + e.Message);
                }

                ViewBag.error = true;
                ViewBag.message = e.Message;
                return View("centers", center);
            }
        }
    }
}
