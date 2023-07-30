using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.Business.Dto;
using WebApi.Business.Services.Abstractions;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly List<Book> _books = new() {
            new Book { Title = "Sunshine", Authors= {}, Id = Guid.NewGuid() },
        };
        public BookService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public BookDto AddBook(BookDto bookDto)
        {
            // var newbook = new Book {Title = bookDto.Title, Authors = bookDto.Authors};
            var newbook = _mapper.Map<Book>(bookDto);
            _books.Add(newbook);
            return bookDto;
        }

        public BookDto DeleteBook(Guid id)
        {
            throw new NotImplementedException();
        }

        public BookDto GetBookById(Guid id)
        {
            var foundBook = _books.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Error not found");
            var bookDto = _mapper.Map<BookDto>(foundBook);
            return bookDto;
        }

        public BookDto UpdateBook(Guid id, BookDto bookDto)
        {
            throw new NotImplementedException();
        }
    }
}