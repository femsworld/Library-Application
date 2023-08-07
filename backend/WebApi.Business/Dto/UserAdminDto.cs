using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Business.Dto
{
    public class UserAdminDto
    {
         public string? Name { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public string? Password { get; set; } 
        public Role Role { get; set; }
    }
}