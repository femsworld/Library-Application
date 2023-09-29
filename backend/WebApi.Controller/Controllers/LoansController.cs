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

        public LoansController(ILoanService loanService, IBookService bookService)
        {
            _loanService = loanService;
        }

        [Authorize]
        [HttpPost]
        public async Task<Loan> PlaceLoan([FromBody] LoanDto loanDto)
        {
            var userId = new Guid(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            var processedLoanDto = new LoanDto
            {
                UserId = userId,
                LoanBooks = loanDto.LoanBooks
            };
            
            return await _loanService.PlaceLoanAsync(userId, processedLoanDto);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<IEnumerable<Loan>> GetAllLoans()
        {
            return await _loanService.GetAllLoansAsync();
        }

        [Authorize]
        [HttpPut("return/{loanId}")]
        public async Task<IActionResult> ReturnLoan(Guid loanId)
        {
            try
            {
                await _loanService.ReturnLoanAsync(loanId);
                // return Ok(loanId);
                var response = new { LoanId = loanId };
                return Ok(response);
            }
            catch (Exception)
            {
                return NotFound("Loan not found");
            }
        }

        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<IEnumerable<Loan>> GetLoansByUserId(Guid userId)
        {
            return await _loanService.GetLoansByUserIdAsync(userId);
        }    
    }
}
