using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterfood.Core.Objects.Generals
{
    public class UploadResponse
    {
        public bool Success { get; set; }
        public string FileName { get; set; } = null!;
    }
}
