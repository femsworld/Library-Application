using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.Dto;
using WebApi.Business.Services.Abstractions;
using WebApi.Domain.Entities;

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

        // private readonly List<string> _books = new() {"Ake", "Ibadan", "Horses", "Orile", "Ijoko"};

        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        // public ActionResult<GetAllBookResponse>  GetAllBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 2)
        public IActionResult GetAllBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        {
            // if (page < 0 || pageSize < 0)
            // {
            //     return BadRequest("page or pageSize cannot be negative");
            // }
            // if (page == 0)
            // {
            //     return Ok (new GetAllBookResponse(1, _books));
            // }
            // var result = _books.Skip((page-1)*pageSize).Take(pageSize).ToList();
            // var totalPages = _books.Count/pageSize;
            // if (_books.Count % pageSize !=0 ) totalPages += 1;
            // var response = new GetAllBookResponse(totalPages, result);
            // return response;

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
            // var foundBook = _bookService.GetBookById(id);
            return _bookService.GetBookById(id);
        }

        // [HttpPost]
        // public IEnumerable<string> CreateBook([FromBody] string name)
        // {
        //     _books.Add(name);
        //     return _books;
        // }

        [HttpPost]
        public BookDto AddBook([FromBody] BookDto bookDto)
        {
             return _bookService.AddBook(bookDto);
        }

        [HttpPatch("{index:int}")]
        // public IEnumerable<string> UpdateBook([FromRoute] int index, [FromBody] string name)
        public BookDto UpdateBook([FromRoute] Guid id, [FromBody] BookDto update)
        {
            return _bookService.UpdateBook(id, update);
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