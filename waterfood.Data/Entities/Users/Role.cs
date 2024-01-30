using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterfood.Data.Entities.Users
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string Title { get; set; } = null!;
        public bool State { get; set; }

        #region Inverse
        [InverseProperty("Role")] public virtual List<Users.User> Users { get; set; } = null!;

        #endregion
    }
}
