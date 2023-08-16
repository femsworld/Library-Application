using Microsoft.AspNetCore.Mvc;
using WebApi.Business.Dto;
using WebApi.Business.Services.Abstractions;
using WebApi.Domain.Entities;
using WebApi.Business.Services.Shared;

namespace WebApi.Controller.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        // public ActionResult<GetAllBookResponse>  GetAllBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 2)
        public IActionResult GetAllBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        {
            if (page < 0 || pageSize < 0)
            {
                return BadRequest("page and pageSize must be positive integers.");
            }
            // if (page == 0)
            // {
            //     return Ok (new GetAllBookResponse(1, _bookService.GetAllBooks()));
            // }
            var books = _bookService.GetAllBooks();
            var totalBooks = books.Count();
            var totalPages = (int)Math.Ceiling((double)totalBooks / pageSize);

            var result = books.Skip((page - 1) * pageSize).Take(pageSize);

            var response = new GetAllBookResponse(totalPages, result);
            return Ok(response);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public BookDto  GetBookById(Guid id)
        {
            return _bookService.GetBookById(id);
        }

        [HttpPost]
        public BookDto AddBook([FromBody] BookDto bookDto)
        {
             return _bookService.AddBook(bookDto);
        }

        [HttpPatch("{id:Guid}")]
        public BookDto UpdateBook([FromRoute] Guid id, [FromBody] BookDto update)
        {
            return _bookService.UpdateBook(id, update);
        }

        [HttpGet("search")]
        public IActionResult SearchBooksByTitle([FromQuery] string searchTerm)
        {
            var books = _bookService.SearchBooksByTitle(searchTerm);
            return Ok(books);
        }

        [HttpGet("categorize")]
        public IActionResult CategorizeBooksByGenre([FromQuery] Genre genre)
        {
            var books = _bookService.CategorizeBooksByGenre(genre);
            return Ok(books);
        }

        [HttpGet("sort")]
        public IActionResult GetSortedBooks([FromQuery] SortOrder sortOrder)
        {
            var books = _bookService.GetSortedBooks(sortOrder);
            return Ok(books);
        }

    }

    public class GetAllBookResponse
    {
        public int TotalPages { get; set; }
         public IEnumerable<BookDto> Books { get; set; }
        public GetAllBookResponse(int totalPages, IEnumerable<BookDto> books)
        {
            TotalPages = totalPages;
            Books = books;
        }
    }
}