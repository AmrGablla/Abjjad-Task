using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Application.DTOs.Account
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; } 
        public string JWToken { get; set; } 
    }
}
