using WebApi.Business.RepoAbstractions;
namespace WebApi.Infrastructure.RepoImplementations
{
    public class CartRepo : ICartRepo
    {
        private readonly Dictionary<Guid, List<Guid>> _userCarts;

        public CartRepo()
        {
            _userCarts = new Dictionary<Guid, List<Guid>>();
        }

        public async Task<int> AddToCartAsync(Guid userId, Guid bookId)
        {
            if (!_userCarts.ContainsKey(userId))
            {
                _userCarts[userId] = new List<Guid>();
            }
            
            _userCarts[userId].Add(bookId);
            return _userCarts[userId].Count;
        }

        public async Task ClearCartAsync(Guid userId)
        {
            if (_userCarts.ContainsKey(userId))
            {
                _userCarts[userId].Clear();
            }
        }

        public async Task<List<Guid>> GetCartContentsAsync(Guid userId)
        {
            return _userCarts.ContainsKey(userId) ? _userCarts[userId] : new List<Guid>();
        }

        public async Task RemoveFromCartAsync(Guid userId, Guid bookId)
        {
            if (_userCarts.ContainsKey(userId))
            {
                _userCarts[userId].Remove(bookId);
            }
        }
    }
}