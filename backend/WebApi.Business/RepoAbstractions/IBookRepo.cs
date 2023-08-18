using WebApi.Business.Dto;
using WebApi.Domain.Entities;
using WebApi.Business.Services.Shared;

namespace WebApi.Business.RepoAbstractions
{
    public interface IBookRepo
    {
        IEnumerable<Book> GetAllBooks();
        Book AddBook(Book book);
        Book GetBookById(Guid id);
        Book UpdateBook(Book book, Book update);
        Book DeleteBook(Guid id);
        IEnumerable<BookDto> SearchBooksByTitle(string searchTerm);
        IEnumerable<BookDto> CategorizeBooksByGenre(Genre genre);
        IEnumerable<BookDto> GetSortedBooks(SortOrder sortOrder);
    }
}