using WebApi.Business.Dto;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Abstractions
{
    public interface ILoanBookService
    {
       IEnumerable <LoanBook> CreateLoanBook(params LoanBookDto[] loanBookDto);
       void DeleteLoanBooks(IEnumerable<LoanBook> loanBooks);
    }
}