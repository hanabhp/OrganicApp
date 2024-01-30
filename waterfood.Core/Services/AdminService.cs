using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Core.Objects.Enums;
using waterfood.Core.Objects.Generals;
using waterfood.Core.Services.Interfaces;
using waterfood.Core.Utilities.Generators;
using waterfood.Core.Utilities.Texts;
using waterfood.Data.Context;
using waterfood.Data.Entities.Centers;
using waterfood.Data.Entities.Generals;
using waterfood.Data.Entities.Items;
using waterfood.Data.Entities.Users;
using waterfood.Objects.Generals;

namespace waterfood.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly WaterFoodContext _context;
        private readonly IConfiguration _environment;

        public AdminService(WaterFoodContext context, IConfiguration environment)
        {
            _context = context;
            _environment = environment;
        }

        #region Categories

        public List<Category> GetAllCategories(string? verb = null)
        {
            if (string.IsNullOrEmpty(verb))
            {
                return _context.Categories
                    .Include(x => x.ParentCategory)
                    .ToList();
            }
            else
            {
                return _context.Categories
                    .Include(x => x.ParentCategory)
                    .Where(x => x.Title.Contains(verb) ||
                                x.ParentCategory.Title.Contains(verb))
                    .ToList();
            }
        }

        public void CreateCategory(Category category)
        {

            Create(category);
        }

        public Category UpdateCategory(Category category)
        {
            Update(category);
            return category;
        }

        public void DeleteCategory(Category category)
        {
            _context.Remove(category);
            _context.SaveChanges();
        }

        #endregion

        #region Generals
        public T Create<T>(T entity)
        {
            if (entity == null) return entity;
            _context.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public T Update<T>(T entity)
        {
            if (entity == null) return entity;

            _context.Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public User? ByUserName(string? userName)
        {
            if (string.IsNullOrEmpty(userName)) return null;

            return _context.Users.FirstOrDefault(x => x.UserName == userName);
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.First(x => x.CategoryId == id);
            
        }

        public AppSettings GetAppSettings()
        {
            return new AppSettings()
            {
                SandBoxUrl = _environment.GetValue<string>("Api:SandBoxUrl"),
                ProductionUrl = _environment.GetValue<string>("Api:ProductionUrl"),
                IsSandBox = _environment.GetValue<bool>("Api:IsSandBox"),
            };
        }

        #endregion

        #region Selects

        public List<SelectContent> SelectAllCategories(string? term = null)
        {
            if (!string.IsNullOrEmpty(term))
            {
                return _context.Categories
                    .Where(x => x.Title.Contains(term))
                    .Select(x => new SelectContent()
                    {
                        id = x.CategoryId.ToString(),
                        text = x.Title

                    }).ToList();
            }
            else
            {
                return _context.Categories
                    .Select(x => new SelectContent()
                    {
                        id = x.CategoryId.ToString(),
                        text = x.Title
                    }).ToList();
            }
        }


        #endregion


        #region Roles
        public List<Role> GetAllRoles(string? verb = null)
        {
            if (string.IsNullOrEmpty(verb))
            {
                return _context.Roles
                    .ToList();
            }
            else
            {
                return _context.Roles
                    .Where(x => x.Title.Contains(verb))
                    .ToList();
            }
        }
        public void CreateRole(Role role)
        {
            Create(role);
        }

        public Role GetRoleById(int id)
        {
            return _context.Roles.First(x => x.RoleId == id);
        }

        public Role UpdateRole(Role role)
        {
            Update(role);
            return role;
        }

        public void DeleteRole(Role role)
        {
            _context.Remove(role);
            _context.SaveChanges();
        }
        #endregion
        public void Init()
        {
            var user = _context.Users.First();
            user.Password = Utilities.Security.PasswordHelper.EncodePasswordMd5("1234");

            Update(user);

        }



        #region Centers
        public List<Center> GetAllCenters(string? verb = null)
        {
            if (string.IsNullOrEmpty(verb))
            {
                return _context.Centers
                    .ToList();
            }
            else
            {
                return _context.Centers
                    .Where(x => x.Name.Contains(verb))
                    .ToList();
            }
        }

        public void CreateCenter(Center center)
        {
            Create(center);
        }

        public Center UpdateCenter(Center center)
        {
            Update(center);
            return center;
        }

        public void DeleteCenter(Center center)
        {
            _context.Remove(center);
            _context.SaveChanges();
        }
        public Center GetCenterById(int id)
        {
            return _context.Centers.First(x => x.CenterId == id);

        }
        #endregion
        #region Items
        public List<Item> GetAllItems(string? verb = null)
        {
            if (string.IsNullOrEmpty(verb))
            {
                return _context.Items
                    .ToList();
            }
            else
            {
                return _context.Items
                    .Where(x => x.Name.Contains(verb))
                    .ToList();
            }
        }

        public void CreateItem(Item item)
        {
            Create(item);
        }

        public Item GetItemById(int id)
        {
            return _context.Items.First(x => x.ItemId == id);
        }

        public Item UpdateItem(Item item)
        {
            Update(item);
            return item;
        }

        public void DeleteItem(Item item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }
        #endregion
        #region Users
        public List<User> GetAllUsers(string? verb = null)
        {
            if (string.IsNullOrEmpty(verb))
            {
                return _context.Users
                    .ToList();
            }
            else
            {
                return _context.Users
                    .Where(x => x.UserName.Contains(verb))
                    .ToList();
            }
        }

        public void CreateUser(User user)
        {
            Create(user);
        }

        public User GetUserById(int id)
        {
            return _context.Users.First(x => x.UserId == id);

        }

        public User UpdateUser(User user)
        {
            Update(user); 
            return user;
        }

        public void DeleteUser(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }
        #endregion
        
        public Center SaveNewCenter(IFormFile file, Center center, string? userName)
        {
            var user = _context.Users.First(x => x.UserName == userName);
            var response = SetCenterImage(file);
            
            try
            {
                if (center.CenterId > 0)
                {

                    if (file != null && response.Success)
                    {
                        RemoveCenterImage(center.CenterImage);
                        center.CenterImage = response.Success ? response.FileName : "-";
                    }

                    Update(center);
                }
                else
                {
                    if (user == null) return center;
                    center.CreateDate = DateTime.Now;
                    center.Creator = user.UserId;
                    center.CenterImage = response.Success ? response.FileName : "-";

                    Create(center);
                }
            }
            catch (Exception e)
            {
                RemoveCenterImage(response.FileName);

            }

            return center;

        }

        public void RemoveCenterImage(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/centers", fileName);
                if (System.IO.File.Exists(dirPath))
                {
                    System.IO.File.Delete(dirPath);
                }
            }
        }
        public UploadResponse SetCenterImage(IFormFile file)
        {
            if (file == null)
                return new UploadResponse()
                {
                    FileName = "Empty File.",
                    Success = false
                };

            try
            {
                string ext = Path.GetExtension(file.FileName);
                string name = "center_" + CodeGenerator.GenerateUniqueCode() + ext;
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/centers", name);

                using var stream = new FileStream(imagePath, FileMode.Create);
                file.CopyTo(stream);

                return new UploadResponse()
                {
                    FileName = name,
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new UploadResponse()
                {
                    FileName = e.Message,
                    Success = false
                };
            }
        }

        public Item SaveNewItem(IFormFile file, Item item, string? userName)
        {

            var user = _context.Users.First(x => x.UserName == userName);
            var response = SetItemImage(file);

            try
            {
                if (item.ItemId > 0)
                {

                    if (file != null && response.Success)
                    {
                        RemoveItemImage(item.Image);
                        item.Image = response.Success ? response.FileName : "-";
                    }
                    
                    Update(item);
                }
                else
                {
                    if (user == null) return item;
                    item.Image = response.Success ? response.FileName : "-";

                    Create(item);
                }
            }
            catch (Exception e)
            {
                RemoveItemImage(response.FileName);

            }

            return item;

        }

        public void RemoveItemImage(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/items", fileName);
                if (System.IO.File.Exists(dirPath))
                {
                    System.IO.File.Delete(dirPath);
                }
            }
        }
        public UploadResponse SetItemImage(IFormFile file)
        {
            if (file == null)
                return new UploadResponse()
                {
                    FileName = "Empty File.",
                    Success = false
                };

            try
            {
                string ext = Path.GetExtension(file.FileName);
                string name = "item_" + CodeGenerator.GenerateUniqueCode() + ext;
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/items", name);

                using var stream = new FileStream(imagePath, FileMode.Create);
                file.CopyTo(stream);

                return new UploadResponse()
                {
                    FileName = name,
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new UploadResponse()
                {
                    FileName = e.Message,
                    Success = false
                };
            }
        }
    }
}
