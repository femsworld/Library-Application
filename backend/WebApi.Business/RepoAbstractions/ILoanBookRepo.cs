using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Business.RepoAbstractions
{
    public interface ILoanBookRepo
    {
        IEnumerable <LoanBook> CreateLoanBook(params LoanBook[] loanBook);
    }
}