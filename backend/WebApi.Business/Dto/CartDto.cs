using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Business.Dto
{
    public class CartDto
    {
        public List<BookDto> BooksInCart { get; set; } = new List<BookDto>();
    }

    public class AddToCartRequest
    {
        public Guid BookId { get; set; }
    }

    public class RemoveFromCartRequest
    {
        public Guid BookId { get; set; }
    }
}