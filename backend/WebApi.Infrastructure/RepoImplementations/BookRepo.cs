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
             return _books.Find(id);
        }

        public Book UpdateBook(Book book, Book update)
        {
            // user.Name = update.Name ?? user.Name;
            // user.Email = update.Email ?? user.Email;
            // _context.SaveChanges();
            // return user;

            book.Title = update.Title ?? book.Title;
            // book.Genre = update.Genre ?? book.Genre;
             _context.SaveChanges();
            return book;


            throw new NotImplementedException();
        }
    }
}