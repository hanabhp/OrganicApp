using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterfood.Data.Entities.Generals
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }

        public string Title { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public int Priority { get; set; }
        public bool IsNew { get; set; }

        #region Relations


        #endregion

        #region Inverse

        [InverseProperty("Menu")] public virtual List<MenuItem> MenuItems { get; set; } = null!;

        #endregion
    }
}
