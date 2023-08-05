using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.Dto;
using WebApi.Business.Services.Abstractions;

namespace WebApi.Controller.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public string VerifyCredentials([FromBody] AuthDto auth) // make it into Dto
        {
            return _authService.VerifyCredentials(auth);
        }
    }
}