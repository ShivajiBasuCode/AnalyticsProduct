using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.Identity
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtTokenOptions
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public int ExpireDays { get; set; }
        public string Audience { get; set; }
    }
}
