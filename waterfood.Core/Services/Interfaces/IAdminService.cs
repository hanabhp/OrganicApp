using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Core.Objects.Generals;
using waterfood.Data.Entities.Centers;
using waterfood.Data.Entities.Generals;
using waterfood.Data.Entities.Items;
using waterfood.Data.Entities.Users;
using waterfood.Objects.Generals;

namespace waterfood.Core.Services.Interfaces
{
    public interface IAdminService
    {
        List<Category> GetAllCategories(string? verb = null);
        User? ByUserName(string? userName);
        AppSettings GetAppSettings();
        T Update<T>(T entity);
        T Create<T>(T entity);
        void DeleteCategory(Category category);
        Category UpdateCategory(Category category);
        void CreateCategory(Category category);
        Category GetCategoryById(int id);
        List<SelectContent> SelectAllCategories(string? term = null);
        List<Role> GetAllRoles(string? verb = null);
        void CreateRole(Role role);
        Role GetRoleById(int id);
        Role UpdateRole(Role role);
        void DeleteRole(Role role);

        List<Center> GetAllCenters(string? verb = null);
        UploadResponse SetCenterImage(IFormFile file);
        void CreateCenter(Center center);
        Center GetCenterById(int id);
        Center UpdateCenter(Center center);
        void DeleteCenter(Center center);

        List<Item> GetAllItems(string? verb = null);
        void CreateItem(Item item);
        Item GetItemById(int id);
        Item UpdateItem(Item item);
        void DeleteItem(Item item);

        List<User> GetAllUsers(string? verb = null);
        void CreateUser(User user);
        User GetUserById(int id);
        User UpdateUser(User user);
        void DeleteUser(User user);
        void Init();


        Center SaveNewCenter(IFormFile file, Center center, string? userName);
        Item SaveNewItem(IFormFile file, Item item, string? userName);
    }
}
