
namespace WebApi.Business.RepoAbstractions
{
    public interface ICartRepo
    {
        Task<int> AddToCartAsync(Guid userId, Guid bookId);
        Task RemoveFromCartAsync(Guid userId, Guid bookId);
        Task<List<Guid>> GetCartContentsAsync(Guid userId);
        Task ClearCartAsync(Guid userId);
    }
}