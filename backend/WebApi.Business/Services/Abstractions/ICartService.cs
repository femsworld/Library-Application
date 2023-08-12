namespace WebApi.Business.Services.Abstractions
{
    public interface ICartService
    {
        // void AddToCart(Guid userId, Guid bookId);
        int AddToCart(Guid userId, Guid bookId);
        void RemoveFromCart(Guid userId, Guid bookId);
        List<Guid> GetCartContents(Guid userId);
        void ClearCart(Guid userId);
    }
}