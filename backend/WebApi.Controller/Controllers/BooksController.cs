using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Entities;

namespace WebApi.Controller.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly List<string> _books = new() {"Ake", "Ibadan", "Horses", "Orile", "Ijoko"};

        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public ActionResult<GetAllBookResponse>  GetAllBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 2)
        {
            if (page < 0 || pageSize < 0)
            {
                return BadRequest("page or pageSize cannot be negative");
            }
            if (page == 0)
            {
                return Ok (new GetAllBookResponse(1, _books));
            }
            var result = _books.Skip((page-1)*pageSize).Take(pageSize).ToList();
            var totalPages = _books.Count/pageSize;
            if (_books.Count % pageSize !=0 ) totalPages += 1;
            var response = new GetAllBookResponse(totalPages, result);
            return response;
        }

        [HttpPost]
        public IEnumerable<string> CreateBook([FromBody] string name)
        {
            _books.Add(name);
            return _books;
        }

        [HttpPatch("{index:int}")]
        public IEnumerable<string> UpdateBook([FromRoute] int index, [FromBody] string name)
        {
            _books[index] = name;
            return _books;
        }

    }

    public class GetAllBookResponse
    {
        public int TotalPages { get; set; }
        public IEnumerable<string> Books { get; set; }
        public GetAllBookResponse(int totalPages, IEnumerable<string> books)
        {
            TotalPages = totalPages;
            Books = books;
        }
    }
}