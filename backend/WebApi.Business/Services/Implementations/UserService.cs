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

            // var user = _mapper.Map<User>(userDto);
            // user.Password = Encoding.UTF8.GetBytes(userDto.Password);
            // var createdUser = _userRepo.CreateUser(user);
            // var createdUserDto = _mapper.Map<UserDto>(createdUser);
            // createdUserDto.Password = Encoding.UTF8.GetString(createdUser.Password);
            // return createdUserDto;
        }


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
            // var foundUser = _users.Find(x => x.Id == id);
            // if (foundUser is null)
            // {
            //     throw new Exception("Error not found");
            // }
            // var userDto = _mapper.Map<UserDto>(foundUser);
            // return userDto;
            var foundUser = _userRepo.GetUserById(id);
            return _mapper.Map<UserDto>(foundUser);
        }

        public UserDto UpdateUser(Guid id, UserDto userDto)
        {
            // var foundUser = _userRepo.GetUserById(id);
            // if (userDto.Name == null || (userDto.Name == ""))
            // {
            //     userDto.Name = foundUser.Name;
            // }
            // if (userDto.Email == null || (userDto.Email == ""))
            // {
            //     userDto.Email = foundUser.Email;
            // }
            // var updatedUser = _userRepo.UpdateUser(foundUser, userDto);
            // var updatedDto= _mapper.Map<UserDto>(updatedUser);
            // updatedDto.Password = userDto.Password;
            // return updatedDto;
            var userToUpdate = _userRepo.GetUserById(id);
            if (userToUpdate == null)
            {
                return null; // or throw an exception if desired
            }

            var updateUser = _mapper.Map<User>(userDto);
            userToUpdate = _userRepo.UpdateUser(userToUpdate, updateUser);
            return _mapper.Map<UserDto>(userToUpdate);

            // throw new NotImplementedException();
        }
    }
}