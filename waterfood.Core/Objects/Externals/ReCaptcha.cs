using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace waterfood.Core.Objects.Externals
{
    public class ReCaptchaSettings
    {
        public string SiteKey { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
    }

    public class GoogleResponse
    {
        [BindProperty(Name = "success")]
        public bool Success { get; set; }
        public double score { get; set; }
        public string action { get; set; } = null!;
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; } = null!;
        [BindProperty(Name = "error-codes")]
        public string[] Errors { get; set; } = null!;
    }
}
