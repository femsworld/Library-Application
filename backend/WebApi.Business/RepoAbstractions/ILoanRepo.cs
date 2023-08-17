using WebApi.Domain.Entities;

namespace WebApi.Business.RepoAbstractions
{
    public interface ILoanRepo
    {
        Loan PlaceLoan(Loan loan);
        void RemoveLoan(Loan loan);
        IEnumerable<Loan> GetAllLoans();
        Loan GetLoanById(Guid loanId);
        IEnumerable<Loan> GetLoansByUserId(Guid userId);
    }
}