using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Login
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class LoginResponse
    {
        public string access_token { get; set; }
    }
}
