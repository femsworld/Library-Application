using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Business.Dto;
using WebApi.Domain.Entities;

namespace WebApi.Business.RepoAbstractions
{
    public interface IBookRepo
    {
        IEnumerable<Book> GetAllBooks();
        Book AddBook(Book book);
        Book GetBookById(Guid id);
        Book UpdateBook(Book book, Book update);
        Book DeleteBook(Guid id);
    }
}