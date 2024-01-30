using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterfood.Data.Entities.Generals
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }
        public int MenuRef { get; set; }
        public string Title { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public string Url { get; set; } = null!;
        public int Priority { get; set; }
        public bool IsNew { get; set; }

        #region Relations


        #endregion
        [ForeignKey("MenuRef")] public virtual Menu Menu { get; set; } = null!;
        #region Inverse

       

        #endregion
    }
}
