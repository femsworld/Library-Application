using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.Dto;
using WebApi.Business.Services.Abstractions;
using WebApi.Domain.Entities;

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

        
        // [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public IActionResult GetAllUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        {
            if (page < 0 || pageSize < 0)
            {
                return BadRequest("page and pageSize must be positive integers.");
            }
            if (page == 0)
            {
                return Ok (new GetAllUserResponse(1,  _userService.GetAllUsers()));
            }
            var users = _userService.GetAllUsers();
            var totalUsers = users.Count();
            var totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            var result = users.Skip((page - 1) * pageSize).Take(pageSize);

            var response = new GetAllUserResponse(totalPages, result);
            return Ok(response);
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
            // var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            // var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            // var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            // return _userService.GetUserById(new Guid(id));
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return _userService.GetUserById(userId);
            }
            else
            {
                return BadRequest("Unable to obtain the user ID from claims.");
            }
        }

        [HttpPost()]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public UserDto CreateUser([FromBody] UserDto userDto)
        {
            return _userService.CreateUser(userDto);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("admin")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public UserAdminDto CreateUserByAdmin([FromBody] UserAdminDto userAdminDto)
        {
        return _userService.CreateUserByAdmin(userAdminDto);
        }

        [Authorize]
        [HttpPatch("{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public UserUpdateDto UpdateUser([FromRoute] Guid id, [FromBody] UserUpdateDto update)
        {
            return _userService.UpdateUser(id, update);
        }

        [Authorize]
        [HttpPatch("password/{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public UserChangePasswordDto ChangeUserPassword([FromRoute] Guid id, [FromBody] UserChangePasswordDto update)
        {
            return _userService.ChangeUserPassword(id, update);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPatch("admin/{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public UserAdminDto UpdateUserByAdmin([FromRoute] Guid id, [FromBody] UserAdminDto update)
        {
            return _userService.UpdateUserByAdmin(id, update);
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
        public IEnumerable<UserDto> Users { get; set; } // Adjusted property type

        public GetAllUserResponse(int totalPages, IEnumerable<UserDto> users)
        {
            TotalPages = totalPages;
            Users = users;
        }
    }
}