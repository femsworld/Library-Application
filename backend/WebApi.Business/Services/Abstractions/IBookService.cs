using WebApi.Business.Dto;
using WebApi.Business.Services.Shared;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Abstractions
{
    public interface IBookService
    {
        BookDto AddBook(BookDto userDto);
        BookDto GetBookById(Guid id);
        BookDto UpdateBook(Guid id, BookDto bookDto);
        BookDto DeleteBook(Guid id);
         IEnumerable<BookDto> GetAllBooks();
         IEnumerable<BookDto> SearchBooksByTitle(string searchTerm);
        IEnumerable<BookDto> CategorizeBooksByGenre(Genre genre);
        IEnumerable<BookDto> GetSortedBooks(SortOrder sort);
    }
}