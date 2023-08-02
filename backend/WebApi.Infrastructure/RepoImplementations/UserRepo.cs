using Microsoft.EntityFrameworkCore;
using WebApi.Business.Dto;
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

        public User CreateUser(User user)
        {
            _users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User user, User update)
        {
            throw new NotImplementedException();
        }

        public User VerifyCredentials(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}