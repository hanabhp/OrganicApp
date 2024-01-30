using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Core.Objects.Accounts;
using waterfood.Core.Objects.Enums;
using waterfood.Core.Objects.Generals;
using waterfood.Core.Objects.Reserves;
using waterfood.Core.Services.Interfaces;
using waterfood.Data.Context;
using waterfood.Data.Entities.Reserves;

namespace waterfood.Core.Services
{
    public class ReserveService : IReserveService
    {
        private readonly WaterFoodContext _context;

        public ReserveService(WaterFoodContext context)
        {
            _context = context;
        }

        public ReserveItemList CheckUserReservedItemsByQrToken(string qrToken, int centerOwnerId)
        {
          var user = _context.Users.FirstOrDefault(x=> x.QrToken == qrToken);
            if (user != null)
            {
                List<int> centerids = new List<int>();
                var centerListOwnedByeAut = _context.Centers.Where(x => x.OwnerRef == centerOwnerId).ToList();
                foreach (var item in centerListOwnedByeAut)
                {
                    centerids.Add(item.CenterId);
                }
                var temp = _context.Reserves.Include(x => x.Status)
                                            .Include(x => x.ReserveItems)
                                            .Include(x => x.ReserveCenter)
                                            .Where(x => centerids.Contains(x.CenterRef))
                                            .Where(x => x.UserRef == user.UserId)
                                            .FirstOrDefault(x => x.StatusRef == (int)ReserveStatuses.Reserving);


                if (temp != null)
                {
                   var itemList = temp.ReserveItems.ToList();
                    List<ReserveItemsDTO> reserveItems = new List<ReserveItemsDTO>();
                    foreach (var item in itemList)
                    {
                        ReserveItemsDTO newItem = new ReserveItemsDTO();
                        newItem.ItemId = item.ItemRef;
                        newItem.ReserveId = temp.ReserveId;
                        newItem.ItemName = _context.Items.FirstOrDefault(x => x.ItemId == item.ItemRef).Name;
                        newItem.StatusRef = item.StatusRef;
                        reserveItems.Add(newItem);
                    }
                    return new ReserveItemList()
                    { ReserveItems = reserveItems,
                        Message = "",
                        Success = true
                    };
                }
                return new ReserveItemList()
                {
                    Message = "No item Found for this user",
                    Success = false
                };
            }
            return new ReserveItemList()
            {
                Message = "User Not Found",
                Success = false
            };
        }

        public ItemsStatus GetAllItemStatuses()
        {
            var temp = _context.ReserveItemStatuses.ToList();
            List<ItemsStatusDTO> res = new List<ItemsStatusDTO>();
            
            if (temp != null)
            {
                foreach (var item in temp)
                {
                    ItemsStatusDTO status = new ItemsStatusDTO()
                    {
                        StatusId = item.StatusId,
                        StatusName = item.Name
                    };
                    res.Add(status);
                }
               return new ItemsStatus()
                {
                    Statuses = res,
                    Success = true,
                    Message = ""
                };
            }
            else
            {
                return new ItemsStatus()
                {
                    Success = false,
                    Message = "No Status Found"
                };
            }
            
        }

        public Response ReserveAnItem(int itemId , int centerId , CurrentUser user)
        {
            var center = _context.Centers
                .Include(x => x.Items)
                .First(x => x.CenterId == centerId);
            var temp = center.Items.FirstOrDefault(x => x.ItemId == itemId);
            //todo check cd
            if (temp != null)
            {
                //check if there is any item left
                if (temp.Left > 0)
                {
                    //check if user have a reserve open atm
                    if (!_context.Reserves.Include(x => x.Status)
                                            .Where(x => x.UserRef == user.UserId)
                                            .Where(x => x.CenterRef == centerId)
                                            .Any(x => x.StatusRef == (int)ReserveStatuses.Reserving))
                    {
                        
                        var newReserve = new Reserve()
                        {
                            UserRef = user.UserId,
                            StatusRef = (int)ReserveStatuses.Reserving,
                            CenterRef = center.CenterId
                        };
                        Create(newReserve);
                        var newReserveItem = new ReserveItem()
                        {
                            ItemRef = itemId,
                            ReserveRef = newReserve.ReserveId,
                            StatusRef = (int)ReserveItemStatuses.Open

                        };
                        Create(newReserveItem);
                        temp.Left = temp.Left - 1;
                        Update(temp);
                        return new Response()
                        {
                            Message = "reserve Created",
                            Success = true
                        };
                    }
                    else
                    {
                        var reserve = _context.Reserves
                            .Include(x => x.Status)
                            .Include(x => x.ReserveItems)
                            .Where(x => x.StatusRef == (int)ReserveStatuses.Reserving)
                            .FirstOrDefault(x => x.UserRef == user.UserId);
                        if (reserve != null)
                        {
                            if (reserve.ReserveItems.Any(x => x.ItemRef == temp.ItemId))
                            {
                                return new Response()
                                {
                                    Message = "Allready in ur reserve",
                                    Success = false
                                };

                            }
                        }
                        
                        var newReserveItem = new ReserveItem()
                        {
                            ItemRef = itemId,
                            ReserveRef = reserve.ReserveId,
                            StatusRef = (int)ReserveItemStatuses.Open
                        };
                        Create(newReserveItem);
                        temp.Left = temp.Left - 1;
                        Update(temp);
                        return new Response()
                        {
                            Message = "added to your reserve",
                            Success = true
                        };
                    }
                }
                else
                {
                    return new Response()
                    {
                        Message = "not existing any more",
                        Success = false
                    };
                    
                }
            }


            return new Response()
            {
                Message = "Error , try again later",
                Success = false
            };

            
        }

        public Response SetItemStatusByCenterOwner(List<ReserveItemsCheckList> items, int userId)
        {
            if (items != null)
            {
                
                foreach (var item in items)
                {
                    var temp = _context.ReserveItems.Where(x => x.ReserveRef == item.ReserveRef)
                                                    .First(x => x.ItemRef == item.ItemId);
                    temp.StatusRef = item.StatusRef;
                    Update(temp);
                }
                return new Response()
                {
                    Message = "",
                    Success = true
                };
              
            }
            return new Response() {
                Message = "Error",
                Success = false
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

    }
}
