using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Core.Objects.Centers;
using waterfood.Core.Objects.Generals;
using waterfood.Data.Entities.Centers;
using waterfood.Data.Entities.Users;

namespace waterfood.Core.Services.Interfaces
{
    public interface ICenterService
    {
        CenterList GetCenters(int userId);
        SingleCenter GetCenterById(int id,int userId);
        CenterItems GetCenterItemsById(int id);
        CenterList GetFavoriteCenters(int userId);
        Response SetCenterAsFavoriteById(int centerId , int userId);
        Response RemoveCenterAsFavoriteById(int centerId, int userId);
        CentersLocationsResult GetAllCenterLocations(int userId);

        
    }
}
