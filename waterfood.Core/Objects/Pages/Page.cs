using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Data.Entities.Generals;

namespace waterfood.Core.Objects.Pages
{
    public class Page
    {
        public string Title { get; set; } = null!;
        public string SubTitle { get; set; } = null!;
    }

    public class Aside
    {
        public List<Menu> Menus { get; set; } = null!;
    }
}
