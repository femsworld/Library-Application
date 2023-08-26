using WebApi.Business.Dto;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Abstractions
{
    public interface ILoanBookService
    {
        Task<IEnumerable<LoanBook>> CreateLoanBookAsync(params LoanBookDto[] loanBookDto);
        Task DeleteLoanBooksAsync(IEnumerable<LoanBook> loanBooks);
    }
}