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

        public IEnumerable<Loan> GetAllLoans()
        {
            return _loans.Include(l => l.LoanBooks).ToList();
        }

        public Loan GetLoanById(Guid loanId)
        {
            // return _loans.Include(l => l.LoanBooks).SingleOrDefault(l => l.Id == loanId);
            return _loans.Include(l => l.LoanBooks).SingleOrDefault(l => l.Id == loanId) ?? throw new Exception("Loan not found"); 
        }

        public IEnumerable<Loan> GetLoansByUserId(Guid userId)
        {
            return _loans.Include(l => l.LoanBooks)
                         .Where(l => l.UserId == userId)
                         .ToList();
        }

        public Loan PlaceLoan(Loan loan)
        {
            _loans.Add(loan);
            _context.SaveChanges();
            return loan;
        }

        public void RemoveLoan(Loan loan)
        {
            _loans.Remove(loan);
            _context.SaveChanges();
        }
    }
}