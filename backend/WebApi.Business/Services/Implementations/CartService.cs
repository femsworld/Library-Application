using WebApi.Business.Services.Abstractions;

namespace WebApi.Business.Services.Implementations
{
    public class CartService : ICartService
    {
        public void AddToCart(Guid userId, Guid bookId)
        {
            throw new NotImplementedException();
        }

        public void ClearCart(Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<Guid> GetCartContents(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromCart(Guid userId, Guid bookId)
        {
            throw new NotImplementedException();
        }
    }
}