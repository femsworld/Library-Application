using Microsoft.AspNetCore.Mvc;
using WebApi.Business.Dto;
using WebApi.Business.Services.Abstractions;

namespace WebApi.Controller.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // [HttpGet]
        // [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        // [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        // public ActionResult<GetAllUserResponse>  GetAllUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 2)
        // {
        //     if (page < 0 || pageSize < 0)
        //     {
        //         return BadRequest("page or pageSize cannot be negative");
        //     }
        //     if (page == 0)
        //     {
        //         return Ok (new GetAllUserResponse(1, _users));
        //     }
        //     var result = _users.Skip((page-1)*pageSize).Take(pageSize).ToList();
        //     var totalPages = _users.Count/pageSize;
        //     if (_users.Count % pageSize !=0 ) totalPages += 1;
        //     var response = new GetAllUserResponse(totalPages, result);
        //     return response;
        // }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public UserDto GetUserById(Guid id)
        {
            return _userService.GetUserById(id);
        }

        [HttpPost()]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public UserDto CreateUser([FromBody] UserDto userDto)
        {
            return _userService.CreateUser(userDto);
        }

        // [Authorize(Policy = "AdminOnly")]
        [HttpPatch("{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public UserDto UpdateUser([FromRoute] Guid id, [FromBody] UserDto update)
        {
            return _userService.UpdateUser(id, update);
        }
    }

    public class GetAllUserResponse
    {
        public int TotalPages { get; set; }
        public IEnumerable<string> Users { get; set; }
        public GetAllUserResponse(int totalPages, IEnumerable<string> users)
        {
            TotalPages = totalPages;
            Users = users;
        }
    }
}