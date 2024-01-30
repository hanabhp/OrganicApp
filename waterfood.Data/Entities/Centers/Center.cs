using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Data.Entities.Generals;
using waterfood.Data.Entities.Items;
using waterfood.Data.Entities.Users;

namespace waterfood.Data.Entities.Centers
{
    public class Center
    {
        [Key]
        public int CenterId { get; set; }
        public string Name { get; set; } = null!;
        public int OwnerRef { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ScheduledTimeStart { get; set; }
        public DateTime ScheduledTimeEnd { get; set; }
        public int? Creator { get; set; }
        public bool State { get; set; }
        public string CenterImage { get; set; } = null!;
        public int? LocationRef { get; set; }
        public double? Rate { get; set; }
        public double? Distance { get; set; }
        public string? PickupTime { get; set; }

        [ForeignKey("LocationRef")] public virtual Location Location { get; set; } = null!;
        [ForeignKey("OwnerRef")] public virtual User OwnerUser { get; set; } = null!;
        [ForeignKey("Creator")] public virtual User CreatedBy { get; set; } = null!;

        [InverseProperty("ReserveCenter")] public virtual List<Reserves.Reserve> Reserve { get; set; } = null!;
        [InverseProperty("Center")] public virtual ICollection<Item> Items { get; set; } = null!;
        [InverseProperty("Center")] public virtual List<Users.FavoriteCenter> FavoriteCenter { get; set; } = null!;
    }
}
