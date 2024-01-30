
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterfood.Core.Objects.Generals
{
    public class AppSettings
    {
        public bool IsSandBox { get; set; }
        public string SandBoxUrl { get; set; } = null!;
        public string ProductionUrl { get; set; } = null!;
    }
}
