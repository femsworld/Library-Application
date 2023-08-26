using AutoMapper;
using WebApi.Business.Dto;
using WebApi.Business.RepoAbstractions;
using WebApi.Business.Services.Abstractions;
using WebApi.Domain.Entities;
using WebApi.Business.Services.Shared;

namespace WebApi.Business.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepo _bookRepo;

        public BookService(IMapper mapper, IBookRepo bookRepo)
        {
            _mapper = mapper;
            _bookRepo = bookRepo;
        }

        public async Task<BookDto> AddBookAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            var newbook = await _bookRepo.AddBookAsync(book);
            return _mapper.Map<BookDto>(newbook);
        }

        public async Task<BookDto> DeleteBookAsync(Guid id)
        {
            var deleteBook = await _bookRepo.DeleteBookAsync(id);
            return _mapper.Map<BookDto>(deleteBook);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepo.GetAllBooksAsync();
            return books.Select(books => _mapper.Map<BookDto>(books));
        }

        public async Task<BookDto> GetBookByIdAsync(Guid id)
        {
            var foundBook = await _bookRepo.GetBookByIdAsync(id);
            return _mapper.Map<BookDto>(foundBook);
        }

        public async Task<BookDto> UpdateBookAsync(Guid id, BookDto bookDto)
        {
            var bookToUpdate = await _bookRepo.GetBookByIdAsync(id);
            if (bookToUpdate == null)
            {
                return null;
            }
            var updatedBook = _mapper.Map<Book>(bookDto);
            bookToUpdate = await _bookRepo.UpdateBookAsync(bookToUpdate, updatedBook);
            return _mapper.Map<BookDto>(bookToUpdate);
        }

        public async Task<IEnumerable<BookDto>> SearchBooksByTitleAsync(string searchTerm)
        {
            var books = await _bookRepo.SearchBooksByTitleAsync(searchTerm);
            return books.Select(books => _mapper.Map<BookDto>(books));
        }

        public async Task<IEnumerable<BookDto>> CategorizeBooksByGenreAsync(Genre genre)
        {
            var books = await _bookRepo.CategorizeBooksByGenreAsync(genre);
            return books.Select(books => _mapper.Map<BookDto>(books));
        }

        public async Task<IEnumerable<BookDto>> GetSortedBooksAsync(SortOrder sortOrder)
        {
            var books = await _bookRepo.GetSortedBooksAsync(sortOrder);
            return books.Select(books => _mapper.Map<BookDto>(books));
        }
    }
}
