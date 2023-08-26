using Microsoft.EntityFrameworkCore;
using WebApi.Business.RepoAbstractions;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Database;

namespace WebApi.Infrastructure.RepoImplementations
{
    public class LoanBookRepo : ILoanBookRepo
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<LoanBook> _loanBooks;

        public LoanBookRepo(DatabaseContext context)
        {
            _context = context;
            _loanBooks = context.LoanBooks;
        }

        public async Task<IEnumerable<LoanBook>> CreateLoanBookAsync(params LoanBook[] loanBooks)
        {
            _loanBooks.AddRange(loanBooks);
            await _context.SaveChangesAsync();
            return loanBooks;
        }

        public async Task DeleteLoanBooksAsync(IEnumerable<LoanBook> loanBooks)
        {
            _loanBooks.RemoveRange(loanBooks);
            await _context.SaveChangesAsync();
        }
    }
}
