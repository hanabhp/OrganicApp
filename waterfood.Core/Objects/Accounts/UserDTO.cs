using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Core.Objects.Generals;

namespace waterfood.Core.Objects.Accounts
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int RoleRef { get; set; }
        public string UserAvatar { get; set; } = null!;
        public DateTime RegisterDate { get; set; }
        public int? Creator { get; set; }
        public bool State { get; set; }
        public int LocationId { get; set; }
        public string Address { get; set; } = null!;
        public string Latitude { get; set; } = null!;
        public string Longitude { get; set; } = null!;

    }
    public class UserLocationDTO : Response
    {
        public UserDTO User { get; set; } = null!;

    }
    public class UserInfo : Response
    {
        public int UserId { get; set; }
        public string UserAvatar { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!;
    }

}
