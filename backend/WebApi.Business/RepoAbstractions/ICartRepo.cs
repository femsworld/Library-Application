
namespace WebApi.Business.RepoAbstractions
{
    public interface ICartRepo
    {
        int AddToCart(Guid userId, Guid bookId);
        void RemoveFromCart(Guid userId, Guid bookId);
        List<Guid> GetCartContents(Guid userId);
        void ClearCart(Guid userId);
    }
}