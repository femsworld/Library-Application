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
        // public void AddToCart(Guid userId, Guid bookId)
        public int AddToCart(Guid userId, Guid bookId)
        {
            // _cartRepo.AddToCart(userId, bookId);
            var book = _bookRepo.GetBookById(bookId);
            Console.WriteLine($"Book by Id: {bookId}");
    
            if (book != null)
            {
                _cartRepo.AddToCart(userId, bookId);
                var cartItemCount = _cartRepo.GetCartContents(userId);
                return cartItemCount.Count;
            }
            else
            {
                return -1;
            }
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