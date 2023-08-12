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
        public void AddToCart(Guid userId, Guid bookId)
        {
            if(!_userCarts.ContainsKey(userId))
            {
                _userCarts[userId] = new List<Guid>();
            }
            if(!_userCarts.ContainsKey(bookId))
            {
                _userCarts[bookId].Add(bookId);
            }
        }

        public void ClearCart(Guid userId)
        {
            if(!_userCarts.ContainsKey(userId))
            {
                _userCarts[userId].Clear();
            }
        }

        public List<Guid> GetCartContents(Guid userId)
        {
            return _userCarts.ContainsKey(userId) ? _userCarts[userId] : new List<Guid>();
        }

        public void RemoveFromCart(Guid userId, Guid bookId)
        {
            if(!_userCarts.ContainsKey(userId))
            {
                _userCarts[userId].Remove(bookId);
            }
        }
    }
}