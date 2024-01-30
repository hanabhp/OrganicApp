using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Data.Entities.Centers;
using waterfood.Data.Entities.Users;

namespace waterfood.Data.Entities.Generals
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string Address { get; set; } = null!;
        public string Latitude { get; set; } = null!;
        public string Longitude { get; set; } = null!;

        [InverseProperty("Location")] public virtual List<User> Users { get; set; } = null!;
        [InverseProperty("Location")] public virtual List<Center> Centers { get; set; } = null!;
    }
}
