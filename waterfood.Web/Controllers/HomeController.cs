using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using waterfood.Core.Services.Interfaces;

namespace waterfood.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdminService _adminService;

        public HomeController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize]
        public IActionResult Index()
        {
            _adminService.Init();
            return View();
        }

        [Route("/404")]
        public IActionResult NotFound()
        {
            return View();
        }

        [Route("/Admin/Select/SelectAllCategories")]
        public JsonResult SelectAllCategories(string term)
        {
            return Json(_adminService.SelectAllCategories(term));
        }
    }
}
