﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradProj.Application.DTO
{
    public class RegisterDto
    {
       
        public string Name { get; set; } = string.Empty;              
        public string Surname { get; set; } = string.Empty;      
        public string Email { get; set; } = string.Empty;       
        public string Password { get; set; } = string.Empty;

    }
}
