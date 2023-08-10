using System.Text;
using AutoMapper;
using WebApi.Business.Dto;
using WebApi.Business.RepoAbstractions;
using WebApi.Business.Services.Abstractions;
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
        public UserDto CreateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            // user.Password = Encoding.UTF8.GetBytes(userDto.Password);
            var createdUser = _userRepo.CreateUser(user);
            return _mapper.Map<UserDto>(createdUser);
        }

        public UserAdminDto CreateUserByAdmin(UserAdminDto userAdminDto)
        {
            var user = _mapper.Map<User>(userAdminDto);
            var createdUser = _userRepo.CreateUserByAdmin(user);
            return _mapper.Map<UserAdminDto>(createdUser);
        }
        // User IUserService.CreateUserByAdmin(UserAdminDto userAdminDto)
        // {
        //     var user = _mapper.Map<User>(userAdminDto);
        //     var createdUser = _userRepo.CreateUserByAdmin(user);
        //     return _mapper.Map<UserAdminDto>(createdUser);
        // }

        public UserDto DeleteUser(Guid id)
        {
            var deleteUser = _userRepo.DeleteUser(id);
            return _mapper.Map<UserDto>(deleteUser);
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _userRepo.GetAllUsers();
            return users.Select(users => _mapper.Map<UserDto>(users));
        }

        public UserDto GetUserById(Guid id)
        {
            var foundUser = _userRepo.GetUserById(id);
            return _mapper.Map<UserDto>(foundUser);
        }

        public UserUpdateDto UpdateUser(Guid id, UserUpdateDto userUpdateDto)
        {
            var userToUpdate = _userRepo.GetUserById(id);
            if (userToUpdate == null)
            {
                return null;
            }

            var updateUser = _mapper.Map<User>(userUpdateDto);
            userToUpdate = _userRepo.UpdateUser(userToUpdate, updateUser);
            return _mapper.Map<UserUpdateDto>(userToUpdate);
        }

        public UserAdminDto UpdateUserByAdmin(Guid id, UserAdminDto userAdminDto)
        {
            var userToUpdate = _userRepo.GetUserById(id);
            if (userToUpdate == null)
            {
                return null; // or throw an exception if desired
            }
            _mapper.Map(userAdminDto, userToUpdate);
            userToUpdate = _userRepo.UpdateUserByAdmin(userToUpdate, userToUpdate);
            return _mapper.Map<UserAdminDto>(userToUpdate);
             }
    }
}