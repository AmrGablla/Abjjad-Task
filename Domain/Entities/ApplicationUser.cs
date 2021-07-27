using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Settings;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser
    {   
        public int Id { get; set; }
        public string UserName { get; set; } 
        public string PasswordHash { get; set; }
    }
}
