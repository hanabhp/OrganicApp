using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterfood.Data.Entities.Reserves
{
    public class ReserveItemStatus
    {
        [Key]
        public int StatusId { get; set; }

        public string Name { get; set; } = null!;


        [InverseProperty("Status")] public virtual List<ReserveItem> ReserveItems { get; set; } = null!;
    }
}
