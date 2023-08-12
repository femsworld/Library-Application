using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Business.RepoAbstractions
{
    public interface ICartRepo
    {
        void AddToCart(Guid userId, Guid bookId);
        void RemoveFromCart(Guid userId, Guid bookId);
        List<Guid> GetCartContents(Guid userId);
        void ClearCart(Guid userId);
    }
}