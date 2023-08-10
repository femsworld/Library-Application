using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Business.Dto;

namespace WebApi.Business.Services.Abstractions
{
    public interface IBookService
    {
        BookDto AddBook(BookDto userDto);
        // BookDto GetBookById(Guid id);
        BookDto GetBookById(Guid id);
        BookDto UpdateBook(Guid id, BookDto bookDto);
        BookDto DeleteBook(Guid id);
         IEnumerable<BookDto> GetAllBooks();
    }
}