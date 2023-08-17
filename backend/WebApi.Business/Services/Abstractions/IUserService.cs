using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Business.Dto;

namespace WebApi.Business.Services.Abstractions
{
    public interface IUserService
    {
        UserDto CreateUser(UserDto userDto);
        UserAdminDto CreateUserByAdmin(UserAdminDto userAdminDto);
        UserDto GetUserById(Guid id);
        UserUpdateDto UpdateUser(Guid id, UserUpdateDto userUpdateDto);
        UserAdminDto UpdateUserByAdmin(Guid id, UserAdminDto userAdminDto);
        UserDto DeleteUser (Guid id);
        IEnumerable<UserDto> GetAllUsers();
    }
}