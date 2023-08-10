using AutoMapper;
using WebApi.Domain.Entities;

namespace WebApi.Business.Dto
{
    [AutoMap(typeof(User))]
    public class UserDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public string? Avatar { get; set; }
    }

    public class UserUpdateDto
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Avatar { get; set; }
    }
}