using Microsoft.EntityFrameworkCore;
using WebApi.Business.RepoAbstractions;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Database;

namespace WebApi.Infrastructure.RepoImplementations
{
    public class LoanRepo : ILoanRepo
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<Loan> _loans;

        public LoanRepo(DatabaseContext context)
        {
            _context = context;
            _loans = context.Loans;
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await _loans.Include(l => l.LoanBooks).ToListAsync();
        }

        public async Task<Loan> GetLoanByIdAsync(Guid loanId)
        {
            var loan = await _loans.Include(l => l.LoanBooks).SingleOrDefaultAsync(l => l.Id == loanId);
            if (loan == null)
            {
                throw new Exception("Loan not found");
            }
            return loan;
        }

        public async Task<IEnumerable<Loan>> GetLoansByUserIdAsync(Guid userId)
        {
            return await _loans.Include(l => l.LoanBooks)
                               .Where(l => l.UserId == userId)
                               .ToListAsync();
        }

        public async Task<Loan> PlaceLoanAsync(Loan loan)
        {
            _loans.Add(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task RemoveLoanAsync(Loan loan)
        {
            _loans.Remove(loan);
            await _context.SaveChangesAsync();
        }
    }
}
