using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Business.RepoAbstractions;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Database;

namespace WebApi.Infrastructure.RepoImplementations
{
    public class LoanRepo : ILoanRepo
    {
        private readonly DatabaseContext  _context;
        private readonly DbSet<Loan> _loans;

        public LoanRepo(DatabaseContext context)
        {
            _context = context;
            _loans = context.Loans;
        }
        public Loan PlaceLoan(Loan loan)
        {
            _loans.Add(loan);
            _context.SaveChanges();
            return loan;
        }
    }
}