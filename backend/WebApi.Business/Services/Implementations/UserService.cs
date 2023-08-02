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
        // private readonly List<User> _users = new() {
        //     new User { Name = "Admin", Address = { PostCode = "000", State = "Kuo", Street = "Kuo st"}, Email = "admin@mail.com", Password = {}, Role = Role.Admin, Id = Guid.NewGuid(), },
        //     new User { Name = "Femi", Address = { PostCode = "000", State = "Kuo", Street = "Kuo st"}, Email = "femi@mail.com", Password = {}, Role = Role.Client, Id = Guid.NewGuid(), },
        //     new User { Name = "Ade", Address = { PostCode = "000", State = "Kuo", Street = "Kuo st"}, Email = "ade@mail.com", Password = {}, Role = Role.Librarian, Id = Guid.NewGuid(), } 
        // };
        public UserService(IMapper mapper, IUserRepo userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }
        public UserDto CreateUser(UserDto userDto)
        {
            // var user = _mapper.Map<User>(userDto);
            var user = _mapper.Map<User>(userDto);
            // user.Password = Encoding.UTF8.GetBytes(userDto.Password);
            var createdUser = _userRepo.CreateUser(user);
            // var createdUser = new User { Name = userDto.Name, Email = userDto.Email, Password = userDto.Password};
            // var createdUser = _mapper.Map<User>(userDto);
            // _users.Add(createdUser);
            // return userDto;
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
            throw new NotImplementedException();
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
            var foundUser = _userRepo.GetUserById(id);
            if (userDto.Name == null || (userDto.Name == ""))
            {
                userDto.Name = foundUser.Name;
            }
            if (userDto.Email == null || (userDto.Email == ""))
            {
                userDto.Email = foundUser.Email;
            }
            var updatedUser = _userRepo.UpdateUser(foundUser, userDto);
            var updatedDto= _mapper.Map<UserDto>(updatedUser);
            updatedDto.Password = userDto.Password;
            return updatedDto;

            // throw new NotImplementedException();
        }
    }
}