﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPTCOM.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}