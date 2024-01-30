using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Data.Entities.Centers;
using waterfood.Data.Entities.Items;
using waterfood.Data.Entities.Users;

namespace waterfood.Data.Entities.Reserves
{
    public class Reserve
    {
        public int ReserveId { get; set; }
        public int UserRef { get; set; }
        public int StatusRef { get; set; }
        public int CenterRef { get; set; }

        [ForeignKey("CenterRef")] public virtual Center ReserveCenter { get; set; } = null!;
        [ForeignKey("UserRef")] public virtual User ReserveUser { get; set; } = null!;
        [ForeignKey("StatusRef")] public virtual ReserveStatus Status { get; set; } = null!;
        [InverseProperty("Reserve")] public virtual List<ReserveItem> ReserveItems { get; set; } = null!;
    }
}
