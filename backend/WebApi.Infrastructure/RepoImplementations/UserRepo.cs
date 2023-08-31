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

        public async Task<User> ChangeUserPasswordAsync(User user, User updatePassword)
        {
            user.Password = updatePassword.Password ?? user.Password;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.Role = Role.Client;
            _users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> CreateUserByAdminAsync(User user)
        {
            _users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUserAsync(Guid id)
        {
            var userToDelete = await _users.FindAsync(id);
            if (userToDelete != null)
            {
                _users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
            return userToDelete;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _users.FindAsync(id);
        }

        public async Task<User> UpdateUserAsync(User user, User update)
        {
            user.Name = update.Name ?? user.Name;
            user.Avatar = update.Avatar ?? user.Avatar;
            user.Age = update.Age;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserByAdminAsync(User user, User update)
        {
            user.Name = update.Name ?? user.Name;
            user.Email = update.Email ?? user.Email;
            user.Role = update.Role;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> VerifyCredentialsAsync(string email, string password)
        {
            var foundUser = await _users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return foundUser;
        }
    }
}
