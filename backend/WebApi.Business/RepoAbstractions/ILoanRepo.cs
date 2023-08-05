using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Business.RepoAbstractions
{
    public interface ILoanRepo
    {
        // Loan PlaceLoan(Guid userId, IEnumerable<LoanBook> loanBooks);
        Loan PlaceLoan(Loan loan);
    }
}