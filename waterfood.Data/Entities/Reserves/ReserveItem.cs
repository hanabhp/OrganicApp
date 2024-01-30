using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Data.Entities.Generals;
using waterfood.Data.Entities.Items;

namespace waterfood.Data.Entities.Reserves
{
    public class ReserveItem
    {
        [Key]
        public int ReserveItemId { get; set; }
        public int ItemRef { get; set; }
        public int ReserveRef { get; set; }
        public int StatusRef { get; set; }

        [ForeignKey("ItemRef")] public virtual Item ReservedItem { get; set; } = null!;
        [ForeignKey("ReserveRef")] public virtual Reserve Reserve { get; set; } = null!;
        [ForeignKey("StatusRef")] public virtual ReserveItemStatus Status { get; set; } = null!;
    }
}
