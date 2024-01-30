using Microsoft.AspNetCore.Mvc;
using waterfood.Core.Services.Interfaces;
using waterfood.Data.Entities.Centers;
using waterfood.Data.Entities.Items;

namespace waterfood.Web.Controllers
{
    public class ItemController : Controller
    {
        private readonly IAdminService _adminService;

        public ItemController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [Route("/Admin/Items")]
        public IActionResult Items()
        {
            var list = _adminService.GetAllItems();
            return View(list);
        }

        [Route("/Admin/Item")]
        public IActionResult AddItem()
        {
            ViewBag.Centers = _adminService.GetAllCenters();
            ViewBag.Categories = _adminService.GetAllCategories();
            return View();
        }

        [HttpPost]
        public IActionResult CreateItem(Item item)
        {
            _adminService.CreateItem(item);
            return View("Items", _adminService.GetAllItems());
        }

        [Route("/Admin/Item/UpdateItem/{id}")]
        public IActionResult UpdateItem(int id)
        {
            ViewBag.Centers = _adminService.GetAllCenters();
            ViewBag.Categories = _adminService.GetAllCategories();
            var item = _adminService.GetItemById(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult EditCenter(Item item)
        {
            item = _adminService.UpdateItem(item);
            return View("Items", _adminService.GetAllItems());
        }

        [Route("/Admin/Item/DeleteItem/{id}")]
        public IActionResult DeleteItem(int id)
        {
            var item = _adminService.GetItemById(id);
            _adminService.DeleteItem(item);

            return View("Items", _adminService.GetAllItems());
        }

        [HttpPost]
        public IActionResult Search(string verb)
        {
            return View("Items", _adminService.GetAllItems(verb));
        }
        [HttpPost]
        public IActionResult Save(IFormFile file, Item item)
        {
            try
            {
                var model = _adminService.SaveNewItem(file, item, User.Identity?.Name);
                if (model.ItemId > 0)
                {
                    return Redirect("/admin/item/" + model.ItemId + "?error=false&message=success");
                }


                return View("items", model);
            }
            catch (Exception e)
            {
                if (item.ItemId > 0)
                {
                    return Redirect("/admin/item/" + item.ItemId + "?error=true&message=" + e.Message);
                }

                ViewBag.error = true;
                ViewBag.message = e.Message;
                return View("items", item);
            }
        }
    }
}
