using WebApi.Domain.Entities;

namespace WebApi.Business.RepoAbstractions
{
    public interface ILoanBookRepo
    {
        Task<IEnumerable<LoanBook>> CreateLoanBookAsync(params LoanBook[] loanBook);
        Task DeleteLoanBooksAsync(IEnumerable<LoanBook> loanBooks);
    }
}