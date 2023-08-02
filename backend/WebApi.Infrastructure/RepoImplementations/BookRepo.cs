using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Business.Dto;
using WebApi.Business.RepoAbstractions;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Database;

namespace WebApi.Infrastructure.RepoImplementations
{
    public class BookRepo : IBookRepo
    {
        private readonly DbSet<Book> _books;
        private readonly DatabaseContext _context;

        public BookRepo(DatabaseContext context)
        {
            _books = context.Books;
            _context = context;
        }
        public Book AddBook(Book book)
        {
            _books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public Book DeleteBook(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Book GetBookById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Book UpdateBook(Book book, BookDto bookDto)
        {
            throw new NotImplementedException();
        }
    }
}