using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.Business.Dto;
using WebApi.Business.Services.Abstractions;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly List<User> _users = new() {
            new User { Name = "Admin", Address = { PostCode = "000", State = "Kuo", Street = "Kuo st"}, Email = "admin@mail.com", Password = "admin123", Role = Role.Admin, Id = Guid.NewGuid(), },
            new User { Name = "Femi", Address = { PostCode = "000", State = "Kuo", Street = "Kuo st"}, Email = "femi@mail.com", Password = "femi123", Role = Role.Client, Id = Guid.NewGuid(), },
            new User { Name = "Ade", Address = { PostCode = "000", State = "Kuo", Street = "Kuo st"}, Email = "ade@mail.com", Password = "ade123", Role = Role.Librarian, Id = Guid.NewGuid(), } 
        };
        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public UserDto CreateUser(UserDto userDto)
        {
            // var createdUser = new User { Name = userDto.Name, Email = userDto.Email, Password = userDto.Password};
            var createdUser = _mapper.Map<User>(userDto);
            _users.Add(createdUser);
            return userDto;
        }

        public UserDto DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserById(Guid id)
        {
            var foundUser = _users.Find(x => x.Id == id);
            // var userDto = new UserDto { Name = foundUser.Name, Email = foundUser.Email, Password = foundUser.Password };
            var userDto = _mapper.Map<UserDto>(foundUser);
            return userDto;
        }

        public UserDto UpdateUser(Guid id, UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}