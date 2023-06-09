﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class User : IdentityUser
    {
        //public int UserId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Email { get; set; }
        //public string Phone { get; set; }
        //public string Username { get; set; }
        //public string Password { get; set; }
        public string Role { get; set; }
    }
}
