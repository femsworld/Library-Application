using WebApi.Business.Dto;
using WebApi.Domain.Entities;
using WebApi.Business.Services.Shared;

namespace WebApi.Business.RepoAbstractions
{
    public interface IBookRepo
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> AddBookAsync(Book book);
        Task<Book> GetBookByIdAsync(Guid id);
        Task<Book> UpdateBookAsync(Book book, Book update);
        Task<Book> DeleteBookAsync(Guid id);
        Task<IEnumerable<BookDto>> SearchBooksByTitleAsync(string searchTerm);
        Task<IEnumerable<BookDto>> CategorizeBooksByGenreAsync(Genre genre);
        Task<IEnumerable<BookDto>> GetSortedBooksAsync(SortOrder sortOrder);
    }
}