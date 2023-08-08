using AutoMapper;
using WebApi.Domain.Entities;

namespace WebApi.Business.Dto
{
    [AutoMap(typeof(User))]
    public class UserAdminDto
    {
         public string? Name { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public string? Password { get; set; } 
        public Role Role { get; set; }
    }
}