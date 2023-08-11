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
        public BookDto AddBook(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            var newbook = _bookRepo.AddBook(book);
            return _mapper.Map<BookDto>(newbook);
        }

        public BookDto DeleteBook(Guid id)
        {
            var deleteBook = _bookRepo.DeleteBook(id);
            return _mapper.Map<BookDto>(deleteBook);
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            var books = _bookRepo.GetAllBooks();
            return books.Select(books => _mapper.Map<BookDto>(books));
        }

        public BookDto GetBookById(Guid id)
        {
            var foundBook = _bookRepo.GetBookById(id);
            return _mapper.Map<BookDto>(foundBook);
        }

        public BookDto UpdateBook(Guid id, BookDto bookDto)
        {
            var bookToUpdate = _bookRepo.GetBookById(id);
            if (bookToUpdate == null)
            {
                return null;
            }
            var updatedBook = _mapper.Map<Book>(bookDto);
            bookToUpdate = _bookRepo.UpdateBook(bookToUpdate, updatedBook);
            return _mapper.Map<BookDto>(bookToUpdate);
        }

        public IEnumerable<BookDto> SearchBooksByTitle(string searchTerm)
        {
           return _bookRepo.SearchBooksByTitle(searchTerm);
         }

        public IEnumerable<BookDto> CategorizeBooksByGenre(Genre genre)
        {
            return _bookRepo.CategorizeBooksByGenre(genre);
        }

        public IEnumerable<BookDto> GetSortedBooks(SortOrder sortOrder)
        {
            return _bookRepo.GetSortedBooks(sortOrder);
        }
    }
}