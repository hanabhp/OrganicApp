using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Data.Entities.Centers;
using waterfood.Data.Entities.Generals;
using waterfood.Data.Entities.Reserves;

namespace waterfood.Data.Entities.Items
{
    public class Item
    {
        [Key] 
        public int ItemId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int CenterRef { get; set; }
        public int CategoryRef { get; set; }
        public int Left { get; set; }

        [InverseProperty("ReservedItem")] public virtual List<ReserveItem> ReserveItem { get; set; } = null!;
        [ForeignKey("CenterRef")] public virtual Center Center { get; set; } = new Center();
        [ForeignKey("CategoryRef")] public virtual Category Category { get; set; } = new Category();
    }
}
