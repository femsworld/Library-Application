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
            _loanBooks = _context.LoanBooks;
        }

        public IEnumerable<LoanBook> CreateLoanBook(params LoanBook[] loanBooks)
        {
            _loanBooks.AddRange(loanBooks);
            _context.SaveChanges();
            return loanBooks;
        }

        public void DeleteLoanBooks(IEnumerable<LoanBook> loanBooks)
        {
            _loanBooks.RemoveRange(loanBooks);
            _context.SaveChanges();
        }
    }
}