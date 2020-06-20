using System;
using System.Security.Claims;

namespace ABD_cw3.Services
{
    public class AuthenticationService
    {
        public Claim[] Claims { get; set; }
        public string RefreshToken { get; set; }
    }
}
