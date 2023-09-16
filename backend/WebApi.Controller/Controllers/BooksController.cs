using Microsoft.AspNetCore.Mvc;
using WebApi.Business.Dto;
using WebApi.Business.Services.Abstractions;
using WebApi.Business.Services.Shared;
using WebApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

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
       public async Task<IActionResult> GetAllBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 6, [FromQuery] Genre? genre = null, [FromQuery] SortOrder? sortOrder = null, [FromQuery] string search = null)
        {
            if (page < 0 || pageSize < 0)
            {
                return BadRequest("page and pageSize must be positive integers.");
            }

            GetAllBookResponse? response;

            if (page == 0)
            {
                var allBooks = await _bookService.GetAllBooksAsync();
                response = new GetAllBookResponse(1, allBooks.ToList());
            }
            else
            {
                var books = await _bookService.GetAllBooksAsync();

                if (genre.HasValue)
                {
                    books = books.Where(book => book.Genre == genre.Value);
                }

                if (!string.IsNullOrWhiteSpace(search))
                {
                    books = books.Where(book => book.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
                }

                if (sortOrder.HasValue)
                {
                    if (sortOrder == SortOrder.Ascending)
                    {
                        books = books.OrderBy(book => book.Title);
                    }
                    else
                    {
                        books = books.OrderByDescending(book => book.Title);
                    }
                }

                var totalBooks = books.Count();
                var totalPages = (int)Math.Ceiling((double)totalBooks / pageSize);
                var result = books.Skip((page - 1) * pageSize).Take(pageSize);
                response = new GetAllBookResponse(totalPages, result);
            }

            return Ok(response);
        }

        // [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            return Ok(book);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
        {
            var addedBook = await _bookService.AddBookAsync(bookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = addedBook.Id }, addedBook);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookDto update)
        {
            var updatedBook = await _bookService.UpdateBookAsync(id, update);
            if (updatedBook == null)
            {
                return NotFound("Book not found");
            }
            return Ok(updatedBook);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var deletedBook = await _bookService.DeleteBookAsync(id);
            if (deletedBook == null)
            {
                return NotFound("Book not found");
            }
            return Ok(deletedBook);

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
