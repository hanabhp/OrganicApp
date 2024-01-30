using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using waterfood.Core.Objects.Centers;
using waterfood.Core.Services.Interfaces;
using waterfood.Data.Context;
using waterfood.Data.Entities.Centers;
using waterfood.Data.Entities.Users;
using waterfood.Data.Entities.Generals;
using waterfood.Core.Objects.Generals;
using waterfood.Core.Objects.Reserves;
using Microsoft.AspNetCore.Http;
using waterfood.Core.Utilities.Generators;
using System.Reflection.Metadata;
using System.Reflection;

namespace waterfood.Core.Services
{
    public class CenterService : ICenterService
    {
        private readonly WaterFoodContext _context;
        private readonly IAdminService _adminService;

        public CenterService(WaterFoodContext context, IAdminService adminService)
        {
            _context = context;
            _adminService = adminService;
        }

        public CenterItems GetCenterItemsById(int id)
        {
            var result = new CenterItems()
            {
                Items = new List<ItemDTO>()
            };

            var app = _adminService.GetAppSettings();

            var raw = _context.Items
                .Include(x => x.Category)
                .Where(x => x.CenterRef == id)
                .ToList();

            if (raw != null)
            {
                foreach (var item in raw)
                {
                    var temp = new ItemDTO()
                    {
                        Description = item.Description,
                        Image = item.Image,
                        ItemId = item.ItemId,
                        Name = item.Name,
                        Category = item.Category.Title,
                        Left = item.Left
                    };
                    result.Items.Add(temp);
                }
                result.Success = true;
                result.Message = "";
                return result;
            }
            else
            {
                result.Success = false;
                result.Message = "there is no item in this center at this moment";
                return result;
            }

        }
        public SingleCenter GetCenterById(int id, int userId)
        {
            var center = new SingleCenter();
            var raw = _context.Centers
                .Include(x => x.CreatedBy)
                .Include(x => x.OwnerUser)
                .Include(x => x.Location)
                .Include(x => x.Items)
                .ThenInclude(o => o.Category)
                .FirstOrDefault(x => x.CenterId == id);

            var app = _adminService.GetAppSettings();

            if (raw != null)
            {
                center.Success = true;
                center.Message = "";
                center.Center.CenterId = raw.CenterId;
                center.Center.Name = raw.Name;
                center.Center.CreateDate = raw.CreateDate;
                center.Center.ScheduledTimeStart = raw.ScheduledTimeStart;
                center.Center.ScheduledTimeEnd = raw.ScheduledTimeEnd;
                center.Center.IsFavorite = _context.FavoriteCenters.Any(x => x.CenterRef == raw.CenterId && x.UserRef == userId);
                center.Center.Owner = raw.OwnerUser.FirstName + " " + raw.OwnerUser.LastName;
                center.Center.PickupTime = raw.PickupTime;
                center.Center.Distance = raw.Distance;
                center.Center.Rate = raw.Rate;
                center.Center.ItemsLeft = raw.Items.Sum(i => i.Left);
                if (raw.CreatedBy != null)
                {
                    center.Center.Creator = raw.CreatedBy.FirstName + " " + raw.CreatedBy.LastName;
                }
                center.Center.State = raw.State;
                center.Center.CenterImage = raw.CenterImage;
                center.Center.OwnerAvatar = raw.OwnerUser.UserAvatar;
                center.Center.Items = raw.Items.Select(i => new ItemDTO
                {
                    Description = i.Description,
                    ItemId = i.ItemId,
                    Name = i.Name,
                    Image = i.Image,
                    Category = i.Category.Title,
                    Left = i.Left

                }).ToList();

                center.Center.Location = new LocationDTO()
                {
                    Address = raw.Location.Address,
                    Latitude = raw.Location.Latitude,
                    LocationId = raw.Location.LocationId,
                    Longitude = raw.Location.Longitude,
                };

                return center;
            }
            else
            {
                center.Success = false;
                center.Message = "Center not found";
                return center;
            }
        }

        public CenterList GetCenters(int userId)
        {
            var app = _adminService.GetAppSettings();
            var result = new CenterList()
            {
                Centers = _context.Centers
                .Include(x => x.CreatedBy)
                .Include(x => x.Location)
                .Include(x => x.OwnerUser)
                .Include(x => x.Items)
                .ThenInclude(o => o.Category)
                .Select(x => new CenterDTO
                {
                    CenterId = x.CenterId,
                    Creator = x.CreatedBy.FirstName + " " + x.CreatedBy.LastName,
                    Name = x.Name,
                    Owner = x.OwnerUser.FirstName + " " + x.OwnerUser.LastName,
                    CreateDate = x.CreateDate,
                    ScheduledTimeEnd = x.ScheduledTimeEnd,
                    ScheduledTimeStart = x.ScheduledTimeStart,
                    State = x.State,
                    CenterImage =  x.CenterImage,
                    OwnerAvatar = x.OwnerUser.UserAvatar,
                    Distance = x.Distance,
                    PickupTime = x.PickupTime,
                    Rate = x.Rate,
                    ItemsLeft = x.Items.Sum(i => i.Left),
                    Items = x.Items.Select(i => new ItemDTO
                    {
                        Description = i.Description,
                        ItemId = i.ItemId,
                        Name = i.Name,
                        Image = i.Image,
                        Category = i.Category.Title,
                        Left = i.Left
                        
                    }).ToList(),
                    IsFavorite = _context.FavoriteCenters.Any(f=> f.CenterRef == x.CenterId && f.UserRef == userId),
                    Location = new LocationDTO()
                    {
                        Address = x.Location.Address,
                        Latitude = x.Location.Latitude,
                        Longitude = x.Location.Longitude,
                        LocationId = x.Location.LocationId,
                    }
                }).ToList()

            };

            if (result != null)
            {
                result.Success = true;
                return result;
            }

            else
            {
                result.Success = false;
                result.Message = "there is no center at this moment";
                return result;
            }

        }

        public CenterList GetFavoriteCenters(int userId)
        {
            var fav = _context.FavoriteCenters.Where(x => x.UserRef == userId).ToList();
            CenterList res = new CenterList();
            List<CenterDTO> centersList = new List<CenterDTO>();
            foreach (var item in fav)
            {
                var temp = _context.Centers
                .Include(x => x.CreatedBy)
                .Include(x => x.Location)
                .Include(x => x.OwnerUser)
                .Include(x => x.Items)
                .ThenInclude(o => o.Category)
                .FirstOrDefault(x => x.CenterId == item.CenterRef);
                var center = new CenterDTO()
                {
                    CenterId = temp.CenterId,
                    //Creator = temp.CreatedBy.FirstName + " " + temp.CreatedBy.LastName,
                    Name = temp.Name,
                    Owner = temp.OwnerUser.FirstName + " " + temp.OwnerUser.LastName,
                    CreateDate = temp.CreateDate,
                    ScheduledTimeEnd = temp.ScheduledTimeEnd,
                    ScheduledTimeStart = temp.ScheduledTimeStart,
                    State = temp.State,
                    CenterImage = temp.CenterImage,
                    OwnerAvatar = temp.OwnerUser.UserAvatar,
                    Distance = temp.Distance,
                    PickupTime = temp.PickupTime,
                    Rate = temp.Rate,
                    ItemsLeft = temp.Items.Sum(i => i.Left),
                    Items = temp.Items.Select(i => new ItemDTO
                    {
                        Description = i.Description,
                        ItemId = i.ItemId,
                        Name = i.Name,
                        Image = i.Image,
                        Category = i.Category.Title,
                        Left = i.Left

                    }).ToList(),
                    Location = new LocationDTO()
                    {
                        Address = temp.Location.Address,
                        Latitude = temp.Location.Latitude,
                        Longitude = temp.Location.Longitude,
                        LocationId = temp.Location.LocationId,
                    }
                };
                if (temp.CreatedBy != null)
                {
                    center.Creator = temp.CreatedBy.FirstName + " " + temp.CreatedBy.LastName;
                }
                if (center != null)
                {
                    centersList.Add(center);
                }
                
            }
            
            res.Centers = centersList;
            if (res != null)
            {
                res.Success = true;
                return res;
            }
            else
            {
                res.Success = false;
                res.Message = "there is no center at this moment";
                return res;
            }

        }

        public Response SetCenterAsFavoriteById(int centerId, int userId)
        {
            if (!_context.FavoriteCenters.Where(x => x.UserRef == userId).Any(x => x.CenterRef == centerId))
            {
                var fav = new FavoriteCenter()
                {
                    CenterRef = centerId,
                    UserRef = userId
                };
                Create(fav);
                return new Response()
                {
                    Success = true,
                    Message = ""
                };
            }
            return new Response()
            {
                Success = false,
                Message = "error try again later"
            };
        }
        public Response RemoveCenterAsFavoriteById(int centerId, int userId)
        {
            if (_context.FavoriteCenters.Where(x => x.UserRef == userId)
                                        .First(x => x.CenterRef == centerId) != null)
            {
                var temp = _context.FavoriteCenters.Where(x => x.UserRef == userId)
                                        .First(x => x.CenterRef == centerId);
                _context.Remove(temp);
                _context.SaveChanges();
                return new Response()
                {
                    Success = true,
                    Message = ""
                };
            }
            return new Response()
            {
                Success = false,
                Message = "error try again later"
            };
        }

        public T Update<T>(T entity)
        {
            if (entity == null) return entity;

            _context.Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public T Create<T>(T entity)
        {
            if (entity == null) return entity;
            _context.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public CentersLocationsResult GetAllCenterLocations(int userId)
        {
            var raw = _context.Centers.Include(x => x.Location)
                                .Include(x => x.Reserve).ToList();
            CentersLocationsResult res = new CentersLocationsResult();
            foreach (var item in raw)
            {
                var temp = new CentersLocations();
                temp.Latitude = item.Location.Latitude;
                temp.Longitude = item.Location.Longitude;
                temp.Reserve = item.Reserve.Any(x => x.UserRef == userId);
                temp.CenterName = item.Name;
                res.CenterLocation.Add(temp);
            }
            if (res.CenterLocation != null)
            {
                res.Message = "";
                res.Success = true;
                return res;
            }
            else
            {
                res.Message = "Error";
                res.Success = false;
                return res;
            }
           
        }

     

    }
}
