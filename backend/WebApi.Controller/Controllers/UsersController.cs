using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.Dto;
using WebApi.Business.Services.Abstractions;
using WebApi.Business.Services.Shared;

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
        public async Task<IActionResult> GetAllUsers([FromQuery] int page = 1,  [FromQuery] int pageSize = 6, [FromQuery] SortOrder? sortOrder = null, [FromQuery] string search = null)
        {
            if (page < 0 || pageSize < 0)
            {
                return BadRequest("page and pageSize must be positive integers.");
            }
            
            GetAllUserResponse? response;

            if (page == 0)
            {
                var allUsers = await _userService.GetAllUsersAsync();
                response = new GetAllUserResponse(1, allUsers.ToList());
            }

            else
            {
                var users = await _userService.GetAllUsersAsync();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    users = users.Where(user => user.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
                }

                if (sortOrder.HasValue)
                {
                    if (sortOrder == SortOrder.Ascending)
                    {
                        users = users.OrderBy(user => user.Name);
                    }
                    else
                    {
                        users = users.OrderByDescending(user => user.Name);
                    }
                }

                var totalUsers = users.Count();
                var totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);
                var result = users.Skip((page - 1) * pageSize).Take(pageSize);
                response = new GetAllUserResponse(totalPages, result);
            }

            return Ok(response);
        }
        
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                var user = await _userService.GetUserByIdAsync(userId);
                if (user != null)
                {
                    return user;
                }
            }
            return BadRequest("Unable to obtain the user profile.");
        }

        [HttpPost()]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
             var createdUser = await _userService.CreateUserAsync(userDto);
            return Ok(createdUser); 
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("admin")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUserByAdmin([FromBody] UserAdminDto userAdminDto)
        {
            var createdUser = await _userService.CreateUserByAdminAsync(userAdminDto);
            return Ok(createdUser); 

        }

        [Authorize]
        [HttpPatch("{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateDto update)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, update);
            if (updatedUser == null)
            {
                return NotFound("User not found");
            }
            return Ok(updatedUser);
        }

        [Authorize]
        [HttpPatch("password/{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeUserPassword(Guid id, [FromBody] UserChangePasswordDto update)
        {
            var updatedPasswordUser = await _userService.ChangeUserPasswordAsync(id, update);
            if (updatedPasswordUser == null)
            {
                return NotFound("User not found");
            }
            return Ok(updatedPasswordUser);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPatch("admin/{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserByAdmin(Guid id, [FromBody] UserAdminDto update)
        {
            var updatedUser = await _userService.UpdateUserByAdminAsync(id, update);
            if (updatedUser == null)
            {
                return NotFound("User not found");
            }
            return Ok(updatedUser);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deletedUser = await _userService.DeleteUserAsync(id);
            if (deletedUser == null)
            {
                return NotFound("User not found");
            }
            return Ok(deletedUser);

        }
    }

    public class GetAllUserResponse
    {
        public int TotalPages { get; set; }
        public IEnumerable<UserDto> Users { get; set; } 

        public GetAllUserResponse(int totalPages, IEnumerable<UserDto> users)
        {
            TotalPages = totalPages;
            Users = users;
        }
    }
}
