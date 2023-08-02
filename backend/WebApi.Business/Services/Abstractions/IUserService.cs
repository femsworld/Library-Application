using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Business.Dto;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Abstractions
{
    public interface IUserService
    {
        UserDto CreateUser(UserDto userDto);
        UserDto GetUserById(Guid id);
        UserDto UpdateUser(Guid id, UserDto userDto);
        UserDto DeleteUser (Guid id);
        IEnumerable<UserDto> GetAllUsers();
    }
}