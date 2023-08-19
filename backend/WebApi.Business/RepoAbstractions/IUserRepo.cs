using WebApi.Domain.Entities;

namespace WebApi.Business.RepoAbstractions
{
    public interface IUserRepo
    {
         IEnumerable<User> GetAllUsers();
        User GetUserById(Guid id);
        User CreateUser(User user);
        User CreateUserByAdmin(User user);
        User UpdateUser(User user, User update);
        User UpdateUserByAdmin(User user, User update);
        User VerifyCredentials(string email, string password);
        User DeleteUser (Guid id);
        User ChangeUserPassword(User user, User updatePassword);
    }
}