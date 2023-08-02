using AutoMapper;
using WebApi.Business.Dto;
using WebApi.Business.RepoAbstractions;
using WebApi.Business.Services.Abstractions;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepo _bookRepo;
        private readonly List<Book> _books = new() {
            new Book { Title = "Sunshine", Authors= {}, Id = Guid.NewGuid() },
        };
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