using WebApi.Business.RepoAbstractions;
using WebApi.Business.Services.Abstractions;

namespace WebApi.Business.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepo;
        private readonly IBookRepo _bookRepo;

        public CartService(ICartRepo cartRepo, IBookRepo bookRepo)
        {
            _cartRepo = cartRepo;
            _bookRepo = bookRepo;
        }

        public async Task<int> AddToCartAsync(Guid userId, Guid bookId)
        {
            var book = await _bookRepo.GetBookByIdAsync(bookId);
            
            if (book != null)
            {
                await _cartRepo.AddToCartAsync(userId, bookId);
                var cartItemCount = await _cartRepo.GetCartContentsAsync(userId);
                return cartItemCount.Count;
            }
            else
            {
                return -1;
            }
        }

        public async Task ClearCartAsync(Guid userId)
        {
            await _cartRepo.ClearCartAsync(userId);
        }

        public async Task<List<Guid>> GetCartContentsAsync(Guid userId)
        {
            return await _cartRepo.GetCartContentsAsync(userId);
        }

        public async Task RemoveFromCartAsync(Guid userId, Guid bookId)
        {
            await _cartRepo.RemoveFromCartAsync(userId, bookId);
        }
    }
}
