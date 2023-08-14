using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Business.Dto
{
    public class CartDto
    {
        public List<BookDto> BooksInCart { get; set; } = new List<BookDto>();
    }

    public class CartItemDto
    {
        public BookDto Book { get; set; }
        public int Quantity { get; set; }
    }
    public class AddToCartRequest
    {
        public Guid BookId { get; set; }
        // public Book Book { get; set; }
    }

    public class RemoveFromCartRequest
    {
        public Guid BookId { get; set; }
    }
}