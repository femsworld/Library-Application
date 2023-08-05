using WebApi.Business.Dto;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Abstractions
{
    public interface ILoanService
    {
        Loan PlaceLoan(Guid userId, IEnumerable<LoanBookDto> loanBookDtos);
    }
}