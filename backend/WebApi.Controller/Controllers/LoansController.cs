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
        public Loan PlaceLoan([FromBody] IEnumerable<LoanBookDto> loanBookDtos)  
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
             Console.WriteLine($"id: {id}");
             Console.WriteLine("loan controller");
            var userId = new Guid (HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            // _loanService.PlaceLoan(userId, loanBookDtos);
            // return new Loan();
            return _loanService.PlaceLoan(userId, loanBookDtos);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public IEnumerable<Loan> GetAllLoans()
        {
            return _loanService.GetAllLoans();
        }

    }
}