using AutoMapper;
using WebApi.Business.Dto;
using WebApi.Business.RepoAbstractions;
using WebApi.Business.Services.Abstractions;
using WebApi.Business.Services.Shared;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;

        public UserService(IMapper mapper, IUserRepo userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        public async Task<UserChangePasswordDto> ChangeUserPasswordAsync(Guid id, UserChangePasswordDto userChangePasswordDto)
        {
            var userToUpdate = await _userRepo.GetUserByIdAsync(id);
            if (userToUpdate == null)
            {
                return null;
            }
            _mapper.Map(userChangePasswordDto, userToUpdate);
            userToUpdate = await _userRepo.ChangeUserPasswordAsync(userToUpdate, userToUpdate);
            return _mapper.Map<UserChangePasswordDto>(userToUpdate);
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            userDto.Role = Role.Client;

            var user = _mapper.Map<User>(userDto);
            user.Password = userDto.Password;

            var createdUser = await _userRepo.CreateUserAsync(user);
            var createdUserDto = _mapper.Map<UserDto>(createdUser);
            return createdUserDto;
        }

        public async Task<UserAdminDto> CreateUserByAdminAsync(UserAdminDto userAdminDto)
        {
            var user = _mapper.Map<User>(userAdminDto);
            var createdUser = await _userRepo.CreateUserByAdminAsync(user);
            return _mapper.Map<UserAdminDto>(createdUser);
        }

        public async Task<UserDto> DeleteUserAsync(Guid id)
        {
            var deleteUser = await _userRepo.DeleteUserAsync(id);
            return _mapper.Map<UserDto>(deleteUser);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllUsersAsync();
            return users.Select(users => _mapper.Map<UserDto>(users));
        }

        public async Task<IEnumerable<UserDto>> GetSortedUsersAsync(SortOrder sortOrder)
        {
            var users = await _userRepo.GetSortedUsersAsync(sortOrder);
            return users.Select(users => _mapper.Map<UserDto>(users));
        }

        // public async Task<UserDto> GetUserByIdAsync(Guid id)
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var foundUser = await _userRepo.GetUserByIdAsync(id);
            // return _mapper.Map<UserDto>(foundUser);
            return _mapper.Map<User>(foundUser);
        }

        public async Task<IEnumerable<UserDto>> SearchUsersByNamesAsync(string searchTerm)
        {
            var users = await _userRepo.SearchUsersByNamesAsync(searchTerm);
            return users.Select(users => _mapper.Map<UserDto>(users));

        }

        public async Task<UserUpdateDto> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto)
        {
            var userToUpdate = await _userRepo.GetUserByIdAsync(id);
            if (userToUpdate == null)
            {
                return null;
            }

            var updateUser = _mapper.Map<User>(userUpdateDto);
            userToUpdate = await _userRepo.UpdateUserAsync(userToUpdate, updateUser);
            return _mapper.Map<UserUpdateDto>(userToUpdate);
        }

        public async Task<UserAdminDto> UpdateUserByAdminAsync(Guid id, UserAdminDto userAdminDto)
        {
            var userToUpdate = await _userRepo.GetUserByIdAsync(id);
            if (userToUpdate == null)
            {
                return null;
            }
            _mapper.Map(userAdminDto, userToUpdate);
            userToUpdate = await _userRepo.UpdateUserByAdminAsync(userToUpdate, userToUpdate);
            return _mapper.Map<UserAdminDto>(userToUpdate);
        }
    }
}
