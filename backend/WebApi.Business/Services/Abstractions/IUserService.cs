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
        UserChangePasswordDto ChangeUserPassword(Guid id, UserChangePasswordDto userChangePasswordDto);
    }
}