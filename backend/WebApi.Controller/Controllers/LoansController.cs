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
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [Authorize]
        [HttpPost]
        public Loan PlaceLoan([FromBody] LoanDto loanDto)  
        {
            var userId = new Guid(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            var processedLoanDto = new LoanDto
            {
                UserId = userId,
                LoanBooks = loanDto.LoanBooks
            };
            
            return _loanService.PlaceLoan(userId, processedLoanDto);
        }

        // [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public IEnumerable<Loan> GetAllLoans()
        {
            return _loanService.GetAllLoans();
        }

        [Authorize]
        [HttpPut("return/{loanId}")]
        public IActionResult ReturnLoan(Guid loanId)
        {
            try
            {
                _loanService.ReturnLoan(loanId);
                return Ok("Loan returned successfully");
            }
            catch (Exception)
            {
                return NotFound("Loan not found");
            }
        }

        [Authorize]
        [HttpGet("user/{userId}")]
        public IEnumerable<Loan> GetLoansByUserId(Guid userId)
        {
            return _loanService.GetLoansByUserId(userId);
        }    
    }
}