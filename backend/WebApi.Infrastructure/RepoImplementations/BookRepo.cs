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

        public async Task<Book> AddBookAsync(Book book)
        {
            _books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> DeleteBookAsync(Guid id)
        {
            var bookToDelete = await _books.FindAsync(id);
            if (bookToDelete != null)
            {
                _books.Remove(bookToDelete);
                await _context.SaveChangesAsync();
            }
            return bookToDelete;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _books.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            Console.WriteLine($"From GetBookById BookRepo: {id}");
            return await _books.FindAsync(id);
        }

        public async Task<Book> UpdateBookAsync(Book book, Book update)
        {
            book.Title = update.Title ?? book.Title;
            book.Genre = update.Genre;
            book.Images = update.Images;
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<BookDto>> SearchBooksByTitleAsync(string searchTerm)
        {
            var books = await _context.Books
                .Where(book => book.Title.Contains(searchTerm))
                .ToListAsync();

            return books.Select(book => _mapper.Map<BookDto>(book)).ToList();
        }

        public async Task<IEnumerable<BookDto>> CategorizeBooksByGenreAsync(Genre genre)
        {
            var books = await _context.Books
                .Where(book => book.Genre == genre)
                .ToListAsync();

            return books.Select(book => _mapper.Map<BookDto>(book)).ToList();
        }

        public async Task<IEnumerable<BookDto>> GetSortedBooksAsync(SortOrder sortOrder)
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

            var books = await query.ToListAsync();
            return books.Select(book => _mapper.Map<BookDto>(book)).ToList();
        }
    }
}
