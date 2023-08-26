using WebApi.Business.Dto;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Abstractions
{
    public interface ILoanService
    {
        Task<Loan> PlaceLoanAsync(Guid userId, LoanDto loanDto);
        Task ReturnLoanAsync(Guid loanId);
        Task<IEnumerable<Loan>> GetAllLoansAsync();
        Task<IEnumerable<Loan>> GetLoansByUserIdAsync(Guid userId);
    }
}