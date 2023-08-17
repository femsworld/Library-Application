using WebApi.Domain.Entities;

namespace WebApi.Business.RepoAbstractions
{
    public interface ILoanBookRepo
    {
        IEnumerable <LoanBook> CreateLoanBook(params LoanBook[] loanBook);
        void DeleteLoanBooks(IEnumerable<LoanBook> loanBooks);
    }
}