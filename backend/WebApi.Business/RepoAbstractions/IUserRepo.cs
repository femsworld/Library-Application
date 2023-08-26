using WebApi.Domain.Entities;

namespace WebApi.Business.RepoAbstractions
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
        Task<User> CreateUserByAdminAsync(User user);
        Task<User> UpdateUserAsync(User user, User update);
        Task<User> UpdateUserByAdminAsync(User user, User update);
        Task<User> VerifyCredentialsAsync(string email, string password);
        Task<User> DeleteUserAsync(Guid id);
        Task<User> ChangeUserPasswordAsync(User user, User updatePassword);
        

    }
}