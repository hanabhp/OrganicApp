using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterfood.Data.Entities.Users
{
    public class FavoriteCenter
    {
        [Key]
        public int FavoriteId { get; set; }
        public int UserRef { get; set; }
        public int CenterRef { get; set; }

        [ForeignKey("UserRef")] public virtual User User { get; set; } = null!;
        [ForeignKey("CenterRef")] public virtual Centers.Center Center { get; set; } = null!;
    }
}
