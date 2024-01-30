using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterfood.Data.Entities.Reserves
{
    public class ReserveStatus
    {
        [Key]
        public int StatusId { get; set; }

        public string Name { get; set; } = null!;


        [InverseProperty("Status")] public virtual List<Reserve> Reserve { get; set; } = null!;
    }
}
