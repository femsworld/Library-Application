using Microsoft.EntityFrameworkCore;
using WebApi.Business.RepoAbstractions;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Database;

namespace WebApi.Infrastructure.RepoImplementations
{
    public class UserRepo : IUserRepo
    {
        private readonly DbSet<User> _users;
        private readonly DatabaseContext _context;

        public UserRepo(DatabaseContext context)
        {
            _users = context.Users;
            _context = context;

        }

        public User ChangeUserPassword(User user, User updatePassword)
        {
            user.Password = updatePassword.Password ?? user.Password;
            _context.SaveChanges();
            return user;
        }

        public User CreateUser(User user)
        {
            user.Role = Role.Client;
            _users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User CreateUserByAdmin(User user)
        {
           _users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User DeleteUser(Guid id)
        {
            var userToDelete = _users.Find(id);
            if (userToDelete != null)
            {
                _users.Remove(userToDelete);
            }
            _context.SaveChanges();
            return userToDelete;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users.ToList();
        }

        public User GetUserById(Guid id)
        {
            return _users.Find(id);
        }

        public User UpdateUser(User user, User update)
        {
            user.Name = update.Name ?? user.Name;
            // user.Email = update.Email ?? user.Email;
            user.Avatar = update.Avatar ?? user.Avatar;
            user.Age = update.Age;
            _context.SaveChanges();
            return user;
        }

        public User UpdateUserByAdmin(User user, User update)
        {
            user.Name = update.Name ?? user.Name;
            user.Email = update.Email ?? user.Email;
            user.Role = update.Role;
            _context.SaveChanges();
            return user;
        }

        public User VerifyCredentials(string email, string password)
        {
            var foundUser = _users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return foundUser;
        }
    }
}