using WebApi.Business.Dto;
using WebApi.Business.Services.Shared;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Abstractions
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task<UserAdminDto> CreateUserByAdminAsync(UserAdminDto userAdminDto);
        // Task<UserDto> GetUserByIdAsync(Guid id);
        Task<User> GetUserByIdAsync(Guid id);
        Task<UserUpdateDto> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto);
        Task<UserAdminDto> UpdateUserByAdminAsync(Guid id, UserAdminDto userAdminDto);
        // Task<UserDto> DeleteUserAsync(Guid id);
        Task<UserDeleteDto> DeleteUserAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserChangePasswordDto> ChangeUserPasswordAsync(Guid id, UserChangePasswordDto userChangePasswordDto);
        Task<IEnumerable<UserDto>> SearchUsersByNamesAsync(string searchTerm);
        Task<IEnumerable<UserDto>> GetSortedUsersAsync(SortOrder sortOrder);
    }
}