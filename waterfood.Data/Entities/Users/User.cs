using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using waterfood.Data.Entities.Generals;
using waterfood.Data.Entities.Items;

namespace waterfood.Data.Entities.Users
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int RoleRef { get; set; }
        public string Password { get; set; } = null!;
        public string QrToken { get; set; } = null!;
        public string ActiveCode { get; set; } = null!;
        public string UserAvatar { get; set; } = null!;
        public DateTime RegisterDate { get; set; }
        public int? Creator { get; set; }
        public bool State { get; set; }
        public int? LocationRef { get; set; }

        [ForeignKey("LocationRef")] public virtual Location Location { get; set; } = null!;
        [ForeignKey("RoleRef")] public virtual Role Role { get; set; } = null!;
        
        [InverseProperty("ReserveUser")] public virtual List<Reserves.Reserve> Reserve { get; set; } = null!;
        [InverseProperty("OwnerUser")] public virtual List<Centers.Center> OwnerUsers { get; set; } = null!;
        [InverseProperty("CreatedBy")] public virtual List<Centers.Center> CreatedByUsers { get; set; } = null!;
        [InverseProperty("User")] public virtual List<Users.FavoriteCenter> FavoriteList { get; set; } = null!;

    }
}
