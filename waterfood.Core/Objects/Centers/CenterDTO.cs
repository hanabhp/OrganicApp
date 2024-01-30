using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Core.Objects.Generals;
using waterfood.Data.Entities.Items;

namespace waterfood.Core.Objects.Centers
{
    public class CenterDTO
    {
        public int CenterId { get; set; }
        public string Name { get; set; } = null!;
        public string Owner { get; set; } = null!;
        public ICollection<ItemDTO> Items { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime ScheduledTimeStart { get; set; }
        public DateTime ScheduledTimeEnd { get; set; }
        public string? Creator { get; set; } = null!;
        public bool State { get; set; }
        public string CenterImage { get; set; } = null!;
        public string OwnerAvatar { get; set; } = null!;
        public double? Rate { get; set; }
        public double? Distance { get; set; }
        public string? PickupTime { get; set; }
        public LocationDTO Location { get; set; } = null!;
        public bool IsFavorite { get; set; }
        public int ItemsLeft { get; set; }
    }

    public class LocationDTO
    {
        public int LocationId { get; set; }
        public string Address { get; set; } = null!;
        public string Latitude { get; set; } = null!;
        public string Longitude { get; set; } = null!;
    }

    public class ItemDTO
    {
        public int ItemId { get; set; }
        public int Left { get; set; }
        public string Category { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
    }


    public class CenterList : Response
    {
        public List<CenterDTO> Centers { get; set; } = null!;
    }
    public class SingleCenter : Response
    {
        public CenterDTO Center { get; set; } = new CenterDTO();
    }
    public class CenterItems : Response
    {
        public List<ItemDTO> Items { get; set; } = null!;
    }
    public class FavoriteCentersList
    {
        public int CenterId { get; set; }
    }
    public class CentersLocations
    {
        public int CenterId { get; set; }
        public string CenterName { get; set; } = null!;
        public string Latitude { get; set; } = null!;
        public string Longitude { get; set; } = null!;
        public bool Reserve { get; set; }
    }
    public class CentersLocationsResult : Response
    {
        public List<CentersLocations> CenterLocation { get; set; } = null!;

    }
}
