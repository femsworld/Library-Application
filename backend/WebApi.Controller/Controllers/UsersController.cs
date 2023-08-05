using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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

        
        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [Authorize(Policy = "AdminOnly")]   
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public UserDto GetUserById(Guid id)
        {
            return _userService.GetUserById(id);
        }

        [Authorize]
        [HttpGet("profile")]
        public ActionResult<UserDto> GetProfile()
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            // var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            return _userService.GetUserById(new Guid(id));
        }

        [HttpPost()]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public UserDto CreateUser([FromBody] UserDto userDto)
        {
            return _userService.CreateUser(userDto);
        }

        [Authorize]
        [HttpPatch("{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public UserDto UpdateUser([FromRoute] Guid id, [FromBody] UserDto update)
        {
            return _userService.UpdateUser(id, update);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public IActionResult DeleteUser(Guid id)
        {
           var deleteUser = _userService.DeleteUser(id);
           if (deleteUser == null)
           {
            return NotFound();
           }
           return Ok(deleteUser);
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