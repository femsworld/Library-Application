using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Abstractions
{
    public interface ILoanService
    {
        Loan PlaceLoan(Guid userId, IEnumerable<LoanBook> loanBookDtos);
    }
}