using WebApi.Business.Dto;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Abstractions
{
    public interface ILoanService
    {
        Loan PlaceLoan(Guid userId, LoanDto loanDto);
        void ReturnLoan(Guid loanId);
        IEnumerable<Loan> GetAllLoans();
        IEnumerable<Loan> GetLoansByUserId(Guid userId);
    }
}