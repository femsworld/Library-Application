using WebApi.Domain.Entities;

namespace WebApi.Business.RepoAbstractions
{
    public interface IUserRepo
    {
         IEnumerable<User> GetAllUsers();
        User GetUserById(Guid id);
        User CreateUser(User user);
        User UpdateUser(User user, User update);
        User VerifyCredentials(string email, string password);
    }
}