using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterfood.Data.Entities.Generals
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public int Priority { get; set; }
        public int? Parent { get; set; }

        #region Relations
        [ForeignKey("Parent")] public virtual Category ParentCategory { get; set; } = null!;
        #endregion

        #region Inverse Relations
        [InverseProperty("Category")] public virtual List<Items.Item> Items { get; set; } = null!;
        #endregion
    }
}
