using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace WebApi.Domain.Entities
{
    [AutoMap(typeof(User))]
    public class User : BaseEnity
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public byte[] Password { get; set; } = default!;
        public Address Address { get; set; } = default!;
        public Role Role { get; set; }
    }

    public enum Role {
        Admin,
        Client,
        Librarian
    }
}