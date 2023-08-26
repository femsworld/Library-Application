using WebApi.Business.Dto;
using WebApi.Business.Services.Shared;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Abstractions
{
    public interface IBookService
    {
        Task<BookDto> AddBookAsync(BookDto bookDto);
        Task<BookDto> GetBookByIdAsync(Guid id);
        Task<BookDto> UpdateBookAsync(Guid id, BookDto bookDto);
        Task<BookDto> DeleteBookAsync(Guid id);
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<IEnumerable<BookDto>> SearchBooksByTitleAsync(string searchTerm);
        Task<IEnumerable<BookDto>> CategorizeBooksByGenreAsync(Genre genre);
        Task<IEnumerable<BookDto>> GetSortedBooksAsync(SortOrder sort);
    }
    
}