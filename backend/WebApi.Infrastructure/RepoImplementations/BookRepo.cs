using Microsoft.EntityFrameworkCore;
using WebApi.Business.Dto;
using WebApi.Business.RepoAbstractions;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Database;
using WebApi.Business.Services.Shared;
using AutoMapper;

namespace WebApi.Infrastructure.RepoImplementations
{
    public class BookRepo : IBookRepo
    {
        private readonly DbSet<Book> _books;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;


        public BookRepo(DatabaseContext context, IMapper mapper)
        {
            _books = context.Books;
            _context = context;
            _mapper = mapper;
        }
        public Book AddBook(Book book)
        {
            _books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public Book DeleteBook(Guid id)
        {
            var bookToDelete = _books.Find(id);
            if (bookToDelete != null)
            {
                _books.Remove(bookToDelete);
            }
            _context.SaveChanges();
            return bookToDelete;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books.ToList();
        }

        public Book GetBookById(Guid id)
        {
            Console.WriteLine($"From GetBookById BookRepo: {id}");
            return _books.Find(id);
        }

        public Book UpdateBook(Book book, Book update)
        {
            book.Title = update.Title ?? book.Title;
            book.Genre = update.Genre;
            book.Images = update.Images;
            _context.SaveChanges();
            return book;
        }

        public IEnumerable<BookDto> SearchBooksByTitle(string searchTerm)
        {
            return _context.Books.Where(book => book.Title.Contains(searchTerm))
                               .Select(book => _mapper.Map<BookDto>(book))
                               .ToList();
        }

        public IEnumerable<BookDto> CategorizeBooksByGenre(Genre genre)
        {
            return _context.Books.Where(book => book.Genre == genre)
                               .Select(book => _mapper.Map<BookDto>(book))
                               .ToList();
        }

        public IEnumerable<BookDto> GetSortedBooks(SortOrder sortOrder)
        {
            var query = _context.Books.AsQueryable();

            if (sortOrder == SortOrder.Ascending)
            {
            query = query.OrderBy(book => book.Title);
            }
            else
            {
            query = query.OrderByDescending(book => book.Title);
            }

            return query.Select(book => _mapper.Map<BookDto>(book)).ToList();
        }
    }
}