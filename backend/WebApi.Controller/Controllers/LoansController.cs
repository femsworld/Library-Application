using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.Dto;
using WebApi.Business.Services.Abstractions;
using WebApi.Domain.Entities;

namespace WebApi.Controller.Controllers
{   
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost]
        public Loan PlaceLoan([FromBody] IEnumerable<LoanBookDto> loanBookDtos)  
        {
            var userId = new Guid (HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            _loanService.PlaceLoan(userId, loanBookDtos);
            // return new Loan();
            return _loanService.PlaceLoan(userId, loanBookDtos);

        }
    }
}