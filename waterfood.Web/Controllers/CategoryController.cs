using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using waterfood.Core.Services.Interfaces;
using waterfood.Data.Entities.Generals;

namespace waterfood.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IAdminService _adminService;

        public CategoryController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Route("/Admin/Categories")]       
        public IActionResult Categories()
        {
            var list = _adminService.GetAllCategories();
            return View(list);
        }

        [Route("/Admin/Category")]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _adminService.CreateCategory(category);
            return View("Categories", _adminService.GetAllCategories());
        }

        [Route("/Admin/Categories/UpdateCategory/{id}")]
        public IActionResult UpdateCategory(int id)
        {
            var category = _adminService.GetCategoryById(id);
            return View(category);
        }


        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            category = _adminService.UpdateCategory(category);
            return View("Categories", _adminService.GetAllCategories());
        }

        [Route("/Admin/Categories/DeleteCategory/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _adminService.GetCategoryById(id);
            _adminService.DeleteCategory(category);

            return View("Categories", _adminService.GetAllCategories());
        }

        [HttpPost]
        public IActionResult Search(string verb)
        {
            return View("Categories", _adminService.GetAllCategories(verb));
        }
    }
}
