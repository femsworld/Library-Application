using WebApi.Domain.Entities;

namespace WebApi.Business.RepoAbstractions
{
    public interface ILoanRepo
    {
        Task<Loan> PlaceLoanAsync(Loan loan);
        Task RemoveLoanAsync(Loan loan);
        Task<IEnumerable<Loan>> GetAllLoansAsync();
        Task<Loan> GetLoanByIdAsync(Guid loanId);
        Task<IEnumerable<Loan>> GetLoansByUserIdAsync(Guid userId);
    }
}