using WebApi.Business.RepoAbstractions;
using WebApi.Business.Services.Abstractions;

namespace WebApi.Business.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepo;

        public CartService(ICartRepo cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public void AddToCart(Guid userId, Guid bookId)
        {
            _cartRepo.AddToCart(userId, bookId);
        }

        public void ClearCart(Guid userId)
        {
            _cartRepo.ClearCart(userId);
        }

        public List<Guid> GetCartContents(Guid userId)
        {
           return _cartRepo.GetCartContents(userId);
        }

        public void RemoveFromCart(Guid userId, Guid bookId)
        {
            _cartRepo.RemoveFromCart(userId, bookId);
        }
    }
}